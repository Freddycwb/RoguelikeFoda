using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityHud : MonoBehaviour
{
    private bool fadeOut;

    public BattleEntity entity;

    public TextMeshPro HP;
    public GameObject floatingText;
    public float count;


    private void Start()
    {
        UpdateHP();
    }

    public void EntityTakeDamage(int damage)
    {
        var a = Instantiate(floatingText, transform.position + Vector3.up, transform.rotation);
        a.GetComponent<TextMeshPro>().color = new Color(1, 0.5f, 0, 1);
        a.GetComponent<TextMeshPro>().text = damage.ToString();
        UpdateHP();
    }

    public void EntityHeal(int amount)
    {
        if (amount < 0)
        {
            return;
        }
        var a = Instantiate(floatingText, transform.position + Vector3.up, transform.rotation);
        a.GetComponent<TextMeshPro>().color = new Color(0.5f, 1, 0, 1);
        a.GetComponent<TextMeshPro>().text = amount.ToString();
        UpdateHP();
    }

    public void UpdateHP()
    {
        if (entity.currentHealth > 0)
        {
            HP.text = entity.currentHealth.ToString() + " / " + entity.maxHealth.ToString();
        }
        else
        {
            HP.text = "0 / " + entity.maxHealth.ToString();
            fadeOut = true;
        }
    }

    private void Update()
    {
        if (fadeOut && count < 1)
        {
            count += Time.deltaTime;
            HP.color = new Color(HP.color.r, HP.color.g, HP.color.b, 1 - count);
        }
        if (entity == null)
        {
            Destroy(gameObject);
        }
    }
}
