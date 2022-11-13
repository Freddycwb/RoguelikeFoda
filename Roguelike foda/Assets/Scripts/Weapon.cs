using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    public BattleEntity Target { get; }
    public int AttackValue { get; }
    public int AdditionalDamage { get; set; }

    public IEnumerator Attack(List<BattleEntity> enemies)
    {
        yield return new WaitForEndOfFrame();
    }
}
