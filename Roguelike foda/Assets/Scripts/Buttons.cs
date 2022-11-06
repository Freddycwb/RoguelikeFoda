using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private bool locked;
    private bool gameOver;

    public GameEvent attackPressed;
    public GameEvent door1Pressed;
    public GameEvent door2Pressed;
    public GameEvent restartPressed;

    public GameObject attackBtn;
    public GameObject door1Btn, door2Btn;
    public GameObject restartBtn;

    public GameObject doorSound;

    public void AttackPressed()
    {
        attackPressed.Raise();
    }

    public void Door1Pressed()
    {
        door1Pressed.Raise();
    }

    public void Door2Pressed()
    {
        door2Pressed.Raise();
    }

    public void RestartPressed()
    {
        gameOver = false;
        restartPressed.Raise();
    }

    public void NoButtons()
    {
        attackBtn.SetActive(false);
        door1Btn.SetActive(false);
        door2Btn.SetActive(false);
        restartBtn.SetActive(false);
    }

    public void BattleButtons()
    {
        if (gameOver || locked) return;
        NoButtons();
        attackBtn.SetActive(true);
    }

    public void DoorsButtons()
    {
        if (gameOver || locked) return;
        NoButtons();
        Instantiate(doorSound);
        door1Btn.SetActive(true);
        door2Btn.SetActive(true);
    }

    public void RestartButtons()
    {
        if (locked) return;
        NoButtons();
        gameOver = true;
        restartBtn.SetActive(true);
    }

    public void LockButtons()
    {
        NoButtons();
        locked = true;
    }

    public void UnlockButtons()
    {
        locked = false;
    }
}
