using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    private bool locked;
    private bool gameOver;
    [HideInInspector]
    public GameObject item1, item2;
    public bool showItemDescription;
    private float count;

    public GameEvent attackPressed;
    public GameEvent door1Pressed;
    public GameEvent door2Pressed;
    public GameEvent item1Pressed;
    public GameEvent item2Pressed;
    public GameEvent restartPressed;

    public GameObject attackBtn;
    public GameObject door1Btn, door2Btn;
    public GameObject item1Btn, item2Btn;
    public GameObject restartBtn;

    public TextMeshPro itemName;
    public TextMeshPro itemDescription;
    public TextMeshPro level;

    public GameObjectVariable Player;

    public Image[] itemSlot;
    public string[] itemSlotDescription;
    public int itensEquipped;

    public Screens screens;

    public GameObject doorSound;

    public void AttackPressed()
    {
        attackPressed.Raise();
    }

    public void Door1Pressed()
    {
        NoButtons();
        StartCoroutine("GoToDoor1");
    }

    public void Door2Pressed()
    {
        NoButtons();
        StartCoroutine("GoToDoor2");
    }

    public IEnumerator GoToDoor1()
    {
        Player.Value.GetComponent<SpriteRenderer>().flipX = true;
        if (Player.Value.transform.position.x < 0)
        {
            yield return Player.Value.GetComponent<BattleEntity>().Movement(new Vector3(-3.2f, 1.6f, 0), 4.5f);
        }
        else
        {
            yield return Player.Value.GetComponent<BattleEntity>().Movement(new Vector3(-3.2f, 1.6f, 0), 7);
        }
        yield return screens.StartCoroutine("TransitionFadeIn");
        door1Pressed.Raise();
        Player.Value.transform.position = new Vector3(3.2f, -1.6f, 0);
        Player.Value.GetComponent<BattleEntity>().entityHud.transform.position = Player.Value.transform.position;
        screens.StartCoroutine("TransitionFadeOut");
    }

    public IEnumerator GoToDoor2()
    {
        Player.Value.GetComponent<SpriteRenderer>().flipX = false;
        if (Player.Value.transform.position.x < 0)
        {
            yield return Player.Value.GetComponent<BattleEntity>().Movement(new Vector3(3.2f, 1.6f, 0), 7);
        }
        else
        {
            yield return Player.Value.GetComponent<BattleEntity>().Movement(new Vector3(3.2f, 1.6f, 0), 4.5f);
        }
        yield return screens.StartCoroutine("TransitionFadeIn");
        door2Pressed.Raise();
        Player.Value.transform.position = new Vector3(-3.2f, -1.6f, 0);
        Player.Value.GetComponent<BattleEntity>().entityHud.transform.position = Player.Value.transform.position;
        screens.StartCoroutine("TransitionFadeOut");
    }

    public void Item1Pressed()
    {
        NoButtons();
        item1Pressed.Raise();
        itemSlot[itensEquipped].gameObject.SetActive(true);
        itemSlot[itensEquipped].gameObject.name = item1.name;
        itemSlotDescription[itensEquipped] = item1.GetComponent<Item>().description;
        itemSlot[itensEquipped].sprite = item1.GetComponent<Item>().representation;
        itensEquipped++;
        DoorsButtons();
        HideItem();
    }

    public void Item2Pressed()
    {
        NoButtons();
        item2Pressed.Raise();
        itemSlot[itensEquipped].gameObject.SetActive(true);
        itemSlot[itensEquipped].gameObject.name = item2.name;
        itemSlotDescription[itensEquipped] = item2.GetComponent<Item>().description;
        itemSlot[itensEquipped].sprite = item2.GetComponent<Item>().representation;
        itensEquipped++;
        DoorsButtons();
        HideItem();
    }

    public void RestartPressed()
    {
        gameOver = false;
        restartPressed.Raise();
        itensEquipped = 0;
        foreach (var item in itemSlot)
        {
            item.sprite = null;
            item.gameObject.SetActive(false);
        }
        DoorsButtons();
    }

    public void NoButtons()
    {
        attackBtn.SetActive(false);
        door1Btn.SetActive(false);
        door2Btn.SetActive(false);
        item1Btn.SetActive(false);
        item2Btn.SetActive(false);
        restartBtn.SetActive(false);
    }

    public void BattleButtons()
    {
        if (gameOver || locked) return;
        NoButtons();
    }

    public void DoorsButtons()
    {
        if (gameOver || locked) return;
        NoButtons();
        Instantiate(doorSound);
        door1Btn.SetActive(true);
        door2Btn.SetActive(true);
    }

    public void ItensButtons()
    {
        item1Btn.transform.GetChild(0).GetComponent<Image>().sprite = item1.GetComponent<Item>().representation;
        item2Btn.transform.GetChild(0).GetComponent<Image>().sprite = item2.GetComponent<Item>().representation;
        item1Btn.SetActive(true);
        item2Btn.SetActive(true);
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
        HideAllItens();
    }

    public void UnlockButtons()
    {
        locked = false;
        ShowAllItens();
    }

    public void ShowItem1()
    {
        showItemDescription = true;
        itemName.text = item1.name;
        itemDescription.text = item1.GetComponent<Item>().description;
    }

    public void ShowItem2()
    {
        showItemDescription = true;
        itemName.text = item2.name;
        itemDescription.text = item2.GetComponent<Item>().description;
    }

    public void ShowItemEquipped(int n)
    {
        showItemDescription = true;
        itemName.text = itemSlot[n].name;
        itemDescription.text = itemSlotDescription[n];
    }

    public void HideItem()
    {
        showItemDescription = false;
    }

    public void HideAllItens()
    {
        foreach (var item in itemSlot)
        {
            item.gameObject.SetActive(false);
        }
        level.gameObject.SetActive(false);
    }

    public void ShowAllItens()
    {
        foreach (var item in itemSlot)
        {
            if (item.sprite != null)
            {
                item.gameObject.SetActive(true);
            }
        }
        level.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (showItemDescription && count < 1)
        {
            count += Time.deltaTime;
            itemName.color = new Color(itemName.color.r, itemName.color.g, itemName.color.b, count);
            itemDescription.color = new Color(itemDescription.color.r, itemDescription.color.g, itemDescription.color.b, count);
        }
        else if (!showItemDescription && count > 0)
        {
            count -= Time.deltaTime;
            itemName.color = new Color(itemName.color.r, itemName.color.g, itemName.color.b, count);
            itemDescription.color = new Color(itemDescription.color.r, itemDescription.color.g, itemDescription.color.b, count);
        }
    }

    public void UpdateLevel()
    {
        level.text = "Lv " + Player.Value.GetComponent<BattleEntity>().level.ToString() + " \r\n" + Player.Value.GetComponent<BattleEntity>().xp.ToString() + "/" + Player.Value.GetComponent<BattleEntity>().xpToLevelUp.ToString();
    }
}
