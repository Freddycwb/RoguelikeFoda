using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSword : MonoBehaviour, Weapon
{
    public int attackValue
    {
        get
        {
            return 3;
        }
    }

    IEnumerator Weapon.Attack(List<BattleEntity> enemies)
    {
        yield return enemies[0].StartCoroutine("TakeDamage", attackValue);
    }

}
