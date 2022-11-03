using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    public int attackValue{ get; }

    public IEnumerator Attack(List<BattleEntity> enemies)
    {
        yield return new WaitForEndOfFrame();
    }
}
