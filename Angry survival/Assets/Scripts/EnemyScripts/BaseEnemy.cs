using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IDamagable, IMovable
{
    // Roept andere scripts aan
    public WaveSpawner waveSpawner;

    // Scriptable object voor de enemy data
    public EnemySO enemyData;

    // Main interface die elke andere enemy gaat gebruiken
    private IEnemyBehaviour enemyBehaviour;

    // Standaard variables
    public bool canMove, canAttack;
    public float health, xpChance, attackCooldown;

    // Layermask
    public LayerMask playerLayer;

    // Pakt wat variablen voordat het project start
    public void Awake()
    {
        xpChance = enemyData.dropRatio;
        health = enemyData.enemyHealth;
        playerLayer = LayerMask.GetMask("playerLayer");
        canMove = true;
        canAttack = true;
        attackCooldown = 0.3f;

        // Zorgt ervoor dat de IEnemyBehaviour interface op elke enemy staat die deze script gebruikt
        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemyData);
            CreateEnemy();
        }
        else
        {
            Debug.LogWarning("No IEnemyBehaviour component found on " + gameObject.name);
        }
    }

    // Update voor alle movements van alle enemies
    public void Update()
    {
        enemyBehaviour.Movement();
    }

    // Bro lees de fucking naam
    public void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // So apparently Random.value grabbed numbers between 0 and 1, and last time I checked, 15 is a lot bigger than 1
        // Fuck Random.value, Random.Range the goat
        if (Random.Range(0, 100) <= xpChance)
        {
            Instantiate(enemyData.xpPrefab, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name} dropped an XP orb.");
        }

        Destroy(gameObject);
    }

    // Enemies kunnen damage krijgen
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {health}");

        if (health <= 0)
        {
            Die();
            GameManagerScript.Instance.AddKill();
            GameManagerScript.Instance.enemyAmount--;
            Debug.Log($"{gameObject.name} has met their demise");
        }
    }

    // Zorgt ervoor dat enemies met de speler kunnen colliden
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Trigger is triggeredeth");
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
    //    {
    //        Debug.Log(gameObject.name + " hit " + collision.gameObject.name);

    //        // Mits de speler de IDamagable interface heeft dan pakt hij de TakeDamage functie
    //        collision.gameObject.GetComponent<IDamagable>().TakeDamage(enemyData.enemyDamage);
    //    }
    //}

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            if (canAttack)
            {
                Debug.Log(gameObject.name + " is attacking " + collision.gameObject.name);
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(enemyData.enemyDamage);

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    // Gonna be real with you chief, this function is kinda useless, I don't even know why this is here
    private void CreateEnemy()
    {
        gameObject.name = enemyData.enemyName;

        health = enemyData.enemyHealth;
    }

    // Voor wanneer ik het spel op pauze wilt zetten gooi ik alle movement gewoon op 0
    public void Movable(bool value)
    {
        canMove = true;
    }
}
