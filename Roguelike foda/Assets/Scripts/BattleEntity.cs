using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour
{
    public int maxHealth;
    public Weapon weapon;

    private WaveMovement waveMovement;
    [SerializeField]
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        weapon = GetComponent<Weapon>();
        waveMovement = GetComponent<WaveMovement>();
    }

    public IEnumerator TakeDamage(int damage)
    {
        currentHealth -= damage;
        yield return waveMovement.StartCoroutine("Play");
        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
        }
    }

    public IEnumerator Movement(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

}
