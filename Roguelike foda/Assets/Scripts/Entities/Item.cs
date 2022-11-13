using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private BattleEntity entity;
    private int timesEventWasListen;
    private int accumulatedEffect;

    public Sprite representation;
    public string description;
    public GameObjectVariable Attacker;

    public bool executeActionOnEnabled;
    public int timesToCallResult = 1;
    public int amount;

    public enum AmountType
    {
        none = 0,
        integer,
        percenterOfMyMaxHealth,
        percenterOfMyAttackValue,
        percenterOfExcessDamage,
        percenterOfEnemyAttackValue
    }

    public AmountType amountType;

    public enum Results
    {
        none = 0,
        heal,
        multiplyDamage,
        increaseMaxHealth,
        increaseMaxHealthByItens,
        hitAllEnemies,
        increaseDamage,
        passExcessDamage,
        hitAttacker
    }

    public Results result;

    private void Start()
    {
        entity = GetComponentInParent<BattleEntity>();
        if (executeActionOnEnabled)
        {
            Action();
        }
    }

    public void Action()
    {
        if (entity == null)
        {
            return;
        }
        timesEventWasListen++;
        if (timesEventWasListen >= timesToCallResult)
        {
            switch (result)
            {
                case Results.none:
                    break;
                case Results.heal:
                    entity.StartCoroutine("Heal", CalculateAmount());
                    break;
                case Results.multiplyDamage:
                    entity.DamageVariable.Value *= amount;
                    break;
                case Results.increaseMaxHealth:
                    entity.maxHealth += amount;
                    entity.entityHud.UpdateHP();
                    break;
                case Results.increaseMaxHealthByItens:
                    Debug.Log("increaseMaxHealth");
                    if (executeActionOnEnabled)
                    {
                        Debug.Log("onEnabled");
                        entity.maxHealth += amount * entity.transform.childCount;
                        entity.entityHud.UpdateHP();
                        executeActionOnEnabled = false;
                    }
                    else
                    {
                        Debug.Log("newItem");
                        entity.maxHealth += amount;
                        entity.entityHud.UpdateHP();
                    }
                    break;
                case Results.hitAllEnemies:
                    foreach (var e in entity.enemies)
                    {
                        if (!e.gameObject.activeSelf)
                        {
                            continue;
                        }
                        e.StartCoroutine("TakeDamage", amount);
                    }
                    break;
                case Results.increaseDamage:
                    entity.weapon.AdditionalDamage += amount;
                    accumulatedEffect += amount;
                    break;
                case Results.passExcessDamage:
                    foreach (var e in entity.enemies)
                    {
                        if (e.currentHealth > 0)
                        {
                            e.StartCoroutine("TakeDamage", CalculateAmount());
                            break;
                        }
                    }
                    break;
                case Results.hitAttacker:
                    Attacker.Value.GetComponent<BattleEntity>().StartCoroutine("TakeDamage", CalculateAmount());
                    break;
                default:
                    break;
            }
            timesEventWasListen = 0;
        }
    }

    public void Undo()
    {
        if (entity == null)
        {
            return;
        }
        switch (result)
        {
            case Results.none:
                break;
            case Results.heal:
                entity.StartCoroutine("TakeDamage", CalculateAmount());
                break;
            case Results.multiplyDamage:
                entity.DamageVariable.Value /= amount;
                break;
            case Results.increaseMaxHealth:
                entity.maxHealth -= amount;
                entity.entityHud.UpdateHP();
                break;
            case Results.increaseMaxHealthByItens:
                if (executeActionOnEnabled)
                {
                    entity.maxHealth -= amount * entity.transform.childCount;
                    entity.entityHud.UpdateHP();
                    executeActionOnEnabled = false;
                }
                else
                {
                    entity.maxHealth -= amount;
                    entity.entityHud.UpdateHP();
                }
                break;
            case Results.hitAllEnemies:
                foreach (var e in entity.enemies)
                {
                    if (!e.gameObject.activeSelf)
                    {
                        continue;
                    }
                    e.StartCoroutine("Heal", amount);
                }
                break;
            case Results.increaseDamage:
                entity.weapon.AdditionalDamage -= accumulatedEffect;
                accumulatedEffect = 0;
                break;
            default:
                break;
        }
    }

    public void CheckIfIAttacked()
    {
        if (Attacker.Value == transform.parent.gameObject)
        {
            Action();
        }
    }

    public void CheckIfIWasAttacked()
    {
        if (Attacker.Value != transform.parent.gameObject)
        {
            Action();
        }
    }

    public int CalculateAmount()
    {
        int value = 0;
        switch (amountType)
        {
            case AmountType.none:
                break;
            case AmountType.integer:
                value = amount;
                break;
            case AmountType.percenterOfMyMaxHealth:
                value = Mathf.FloorToInt(entity.maxHealth * amount * 0.01f);
                break;
            case AmountType.percenterOfMyAttackValue:
                value = Mathf.FloorToInt(entity.DamageVariable.Value * amount * 0.01f);
                break;
            case AmountType.percenterOfExcessDamage:
                value = Mathf.FloorToInt(entity.weapon.Target.currentHealth * -1 * amount * 0.01f);
                break;
            case AmountType.percenterOfEnemyAttackValue:
                value = Mathf.FloorToInt(entity.weapon.AttackValue * amount * 0.01f);
                break;
            default:
                break;
        }
        return value;
    }
}
