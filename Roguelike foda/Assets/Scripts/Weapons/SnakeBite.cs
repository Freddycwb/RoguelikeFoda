using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBite : MonoBehaviour, Weapon
{
    public BattleEntity Target
    {
        get
        {
            return target;
        }
    }
    public int AttackValue
    {
        get
        {
            return 4;
        }
    }

    public int AdditionalDamage
    {
        get
        {
            return additionalDamage;
        }
        set
        {
            additionalDamage = value;
        }
    }

    private int additionalDamage;
    private BattleEntity entity;
    private BattleEntity target;
    private GameEvent StartAttack;
    private IntVariable DamageVariable;

    IEnumerator Weapon.Attack(List<BattleEntity> enemies)
    {
        foreach (var e in enemies)
        {
            if (e.currentHealth > 0)
            {
                target = enemies[0];
                break;
            }
        }
        DamageVariable.Value = AttackValue;
        entity.enemies = enemies;
        StartAttack.Raise();
        yield return new WaitForEndOfFrame();
        DamageVariable.Value += additionalDamage;
        if (target.enabled)
        {
            yield return target.StartCoroutine("TakeDamage", DamageVariable.Value);
        }
    }

    private void OnEnable()
    {
        entity = GetComponentInParent<BattleEntity>();
        StartAttack = entity.StartAttack;
        DamageVariable = entity.DamageVariable;
    }
}
