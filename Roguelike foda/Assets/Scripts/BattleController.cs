using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private BattleEntity player;
    private bool battling;

    public GameEvent EndBattle;
    public GameEvent StartTurn;
    public GameEvent EndTurn;
    public GameEvent EndAttack;
    public GameEvent PlayerDeath;
    public GameObjectVariable Player;
    public GameObjectVariable Attacker;
    public GameObjectVariable CurrentRoom;
    public List<BattleEntity> enemies = new List<BattleEntity>();

    public void CallStartBattle()
    {
        enemies = CurrentRoom.Value.GetComponent<Room>().enemiesBattleEntitys;
        player = Player.Value.GetComponent<BattleEntity>();
        battling = true;
        CallStartTurn();
    }

    public void CallStartTurn()
    {
        if (battling)
        {
            StartCoroutine("Turn");
        }
    }

    public IEnumerator Turn()
    {
        StartTurn.Raise();
        yield return new WaitForSeconds(2);
        if (!battling)
        {
            yield break;
        }
        yield return StartCoroutine("PlayerAction");
        if (!battling)
        {
            yield break;
        }
        yield return new WaitForSeconds(2.5f);
        yield return StartCoroutine("EnemysAction");
        EndTurn.Raise();
    }

    public IEnumerator PlayerAction()
    {
        Attacker.Value = player.gameObject;
        Vector3 lastPos = player.transform.position;
        player.idleMovement.stopped = true;
        yield return player.Movement(lastPos + (Vector3.right * 0.8f) + (Vector3.up * 0.4f), 7);
        yield return new WaitForSeconds(0.1f);
        if (battling)
        {
            yield return player.weapon.Attack(enemies);
        }
        yield return new WaitForSeconds(0.1f);
        yield return player.Movement(lastPos, 5);
        player.idleMovement.stopped = false;
        EndAttack.Raise();
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
            if (!battling)
            {
                break;
            }
            enemiesAlive.Add(e);
            Attacker.Value = e.gameObject;
            Vector3 lastPos = e.transform.position;
            e.idleMovement.stopped = true;
            yield return e.Movement(lastPos + (Vector3.left * 0.8f) + (Vector3.down * 0.4f), 7);
            yield return new WaitForSeconds(0.1f);
            yield return e.weapon.Attack(p);
            yield return new WaitForSeconds(0.1f);
            yield return e.Movement(lastPos, 5);
            e.idleMovement.stopped = false;
            yield return new WaitForSeconds(0.5f);
            EndAttack.Raise();
        }
        enemies = enemiesAlive;
    }
    
    public void CheckEndBattle()
    {
        if (player.currentHealth <= 0)
        {
            PlayerDeath.Raise();
            battling = false;
        }
        List<BattleEntity> enemiesAlive = new List<BattleEntity>();
        foreach (var e in enemies)
        {
            if (e.currentHealth > 0)
            {
                enemiesAlive.Add(e);
            }
        }
        enemies = enemiesAlive;
        if (enemies.Count == 0)
        {
            EndBattle.Raise();
            battling = false;
        }
    }
}
