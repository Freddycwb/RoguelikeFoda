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

    private void Start()
    {
        entity = GetComponentInParent<BattleEntity>();
    }

    public enum Results
    {
        none = 0,
        heal
    }

    public Results result;

    public void Action()
    {
        timesEventWasListen++;
        if (timesEventWasListen >= timesToCallResult)
        {
            switch (result)
            {
                case Results.none:
                    break;
                case Results.heal:
                    entity.StartCoroutine("Heal", amount);
                    break;
                default:
                    break;
            }
            timesEventWasListen = 0;
        }
    }

    public void CheckWhoAttacked()
    {
        if (Attacker.Value == transform.parent.gameObject)
        {
            Action();
        }
    }
}
