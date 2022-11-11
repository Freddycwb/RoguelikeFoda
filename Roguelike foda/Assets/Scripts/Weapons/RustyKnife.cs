using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyKnife : MonoBehaviour, Weapon
{
    public int attackValue
    {
        get
        {
            return 2;
        }
    }

    private GameEvent StartAttack;
    private IntVariable DamageVariable;

    IEnumerator Weapon.Attack(List<BattleEntity> enemies)
    {
        DamageVariable.Value = attackValue;
        StartAttack.Raise();
        yield return new WaitForEndOfFrame();
        yield return enemies[0].StartCoroutine("TakeDamage", DamageVariable.Value);
    }

    private void OnEnable()
    {
        StartAttack = GetComponentInParent<BattleEntity>().StartAttack;
        DamageVariable = GetComponentInParent<BattleEntity>().DamageVariable;
    }
}
