using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private BattleEntity entity;
    [SerializeField]
    private int timesEventWasListen;

    public GameObjectVariable Attacker;

    public int timesToCallResult = 1;
    public int amount;

    public enum AmountType
    {
        none = 0,
        integer,
        percenterOfMyMaxHealth,
        percenterOfMyAttackValue
    }

    public AmountType amountType;

    public enum Results
    {
        none = 0,
        heal
    }

    public Results result;

    private void Start()
    {
        entity = GetComponentInParent<BattleEntity>();
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
                default:
                    break;
            }
            timesEventWasListen = 0;
        }
    }

    public void CheckIfIAttacked()
    {
        if (Attacker.Value == transform.parent.gameObject)
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
            default:
                break;
        }
        return value;
    }
}
