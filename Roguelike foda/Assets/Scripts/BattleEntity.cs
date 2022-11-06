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
    public GameObject[] damageSounds;
    public GameObject deathSound;

    void Start()
    {
        currentHealth = maxHealth;
        weapon = GetComponent<Weapon>();
        waveMovement = GetComponent<WaveMovement>();
    }

    public IEnumerator TakeDamage(int damage)
    {
        currentHealth -= damage;
        PlayDamageSound();
        yield return waveMovement.StartCoroutine("Play");
        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
        }
    }

    public IEnumerator Heal(int amount)
    {
        currentHealth = currentHealth + amount > maxHealth ? maxHealth : currentHealth + amount;
        yield return new WaitForEndOfFrame();
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
        Instantiate(deathSound);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

    public void PlayDamageSound()
    {
        int r = Random.Range(0, damageSounds.Length);
        Instantiate(damageSounds[r]);
    }
}
