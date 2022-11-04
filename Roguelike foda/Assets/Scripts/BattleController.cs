using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private BattleEntity player;

    public GameEvent StartBattle;
    public GameEvent EndBattle;
    public GameEvent StartTurn;
    public GameEvent EndTurn;
    public GameEvent PlayerDeath;
    public GameObjectVariable Player;
    public GameObjectVariable CurrentRoom;
    public List<BattleEntity> enemies = new List<BattleEntity>();

    public void CallStartBattle()
    {
        StartBattle.Raise();
        enemies = CurrentRoom.Value.GetComponent<Room>().enemiesBattleEntitys;
        player = Player.Value.GetComponent<BattleEntity>();
    }

    public void CallStartTurn()
    {
        StartCoroutine("Turn");
    }

    public IEnumerator Turn()
    {
        StartTurn.Raise();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("PlayerAction");
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("EnemysAction");
        yield return new WaitForSeconds(0.5f);
        EndTurn.Raise();
        if (enemies.Count <= 0)
        {
            CheckEndBattle();
        }
    }

    public IEnumerator PlayerAction()
    {
        Vector3 lastPos = player.transform.position;
        yield return player.Movement(lastPos + (Vector3.right * 0.8f) + (Vector3.up * 0.4f), 7);
        yield return new WaitForSeconds(0.1f);
        yield return player.weapon.Attack(enemies);
        yield return new WaitForSeconds(0.1f);
        yield return player.Movement(lastPos, 5);
    }

    public IEnumerator EnemysAction()
    {
        List<BattleEntity> p = new List<BattleEntity>();
        List<BattleEntity> enemiesAlive = new List<BattleEntity>();
        p.Add(player);
        foreach (var e in enemies)
        {
            if (!e.gameObject.activeSelf)
            {
                continue;
            }
            enemiesAlive.Add(e);
            Vector3 lastPos = e.transform.position;
            yield return e.Movement(lastPos + (Vector3.left * 0.8f) + (Vector3.down * 0.4f), 7);
            yield return new WaitForSeconds(0.1f);
            yield return e.weapon.Attack(p);
            yield return new WaitForSeconds(0.1f);
            yield return e.Movement(lastPos, 5);
            yield return new WaitForSeconds(0.4f);
            if (CheckEndBattle())
            {
                break;
            }
        }
        enemies = enemiesAlive;
    }
    
    public bool CheckEndBattle()
    {
        if (!player.gameObject.activeSelf)
        {
            PlayerDeath.Raise();
            return true;
        }
        if (enemies.Count == 0)
        {
            EndBattle.Raise();
            return true;
        }
        return false;
    }
}
