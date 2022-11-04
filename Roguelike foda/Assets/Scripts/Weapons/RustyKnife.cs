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

    IEnumerator Weapon.Attack(List<BattleEntity> enemies)
    {
        yield return enemies[0].StartCoroutine("TakeDamage", attackValue);
    }
}
