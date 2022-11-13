using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour
{
    public int maxHealth;
    public Weapon weapon;
    public GameEvent StartAttack;
    public GameEvent EntityDied;
    public IntVariable DamageVariable;
    [HideInInspector]
    public List<BattleEntity> enemies;
    private WaveMovement waveMovement;
    [HideInInspector]
    public EntityHud entityHud;
    [HideInInspector]
    public IdleMovement idleMovement;
    public float currentHealth;
    public GameObject entityHudPrefab;
    public GameObject[] damageSounds;
    public GameObject deathSound;

    void Start()
    {
        currentHealth = maxHealth;
        weapon = GetComponent<Weapon>();
        waveMovement = GetComponent<WaveMovement>();
        idleMovement = GetComponent<IdleMovement>();
        entityHud = Instantiate(entityHudPrefab).GetComponent<EntityHud>();
        entityHud.entity = this;
        entityHud.transform.position = transform.position;
    }

    public IEnumerator TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            yield break;
        }
        currentHealth -= damage;
        PlayDamageSound();
        entityHud.EntityTakeDamage(damage);
        idleMovement.stopped = true;
        yield return waveMovement.StartCoroutine("Play");
        idleMovement.stopped = false;
        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
        }
    }

    public IEnumerator Heal(int amount)
    {
        if (amount <= 0 || currentHealth == maxHealth)
        {
            yield break;
        }
        currentHealth = currentHealth + amount > maxHealth ? maxHealth : currentHealth + amount;
        entityHud.EntityHeal(amount);
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator Movement(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            entityHud.transform.position = transform.position;
            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
        entityHud.transform.position = transform.position;
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(deathSound);
        EntityDied.Raise();
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

    public void PlayDamageSound()
    {
        int r = Random.Range(0, damageSounds.Length);
        Instantiate(damageSounds[r]);
    }
}
