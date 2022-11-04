using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBite : MonoBehaviour, Weapon
{
    public int attackValue
    {
        get
        {
            return 1;
        }
    }

    IEnumerator Weapon.Attack(List<BattleEntity> enemies)
    {
        yield return enemies[0].StartCoroutine("TakeDamage", attackValue);
    }
}
