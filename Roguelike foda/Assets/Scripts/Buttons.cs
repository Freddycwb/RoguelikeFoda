using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private bool gameOver;

    public GameEvent attackPressed;
    public GameEvent doorPressed;
    public GameEvent restartPressed;

    public GameObject attackBtn;
    public GameObject door1Btn, door2Btn;
    public GameObject restartBtn;

    public void AttackPressed()
    {
        attackPressed.Raise();
    }

    public void DoorPressed()
    {
        doorPressed.Raise();
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
        if (gameOver) return;
        NoButtons();
        attackBtn.SetActive(true);
    }

    public void DoorsButtons()
    {
        if (gameOver) return;
        NoButtons();
        door1Btn.SetActive(true);
        door2Btn.SetActive(true);
    }

    public void RestartButtons()
    {
        NoButtons();
        gameOver = true;
        restartBtn.SetActive(true);
    }
}
