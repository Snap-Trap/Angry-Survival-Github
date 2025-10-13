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
    public bool canMove, canInteractPlayer;
    public float health;

    // Layermask

    public LayerMask playerLayer;


    // Pakt wat variablen voordat het project start
    public void Awake()
    {
        playerLayer = LayerMask.GetMask("playerLayer");
        canMove = true;
        canInteractPlayer = true;

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
        Destroy(gameObject);
    }

    // Enemies kunnen damage krijgen
    public void TakeDamage(float damage)
    {
        enemyData.enemyHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {enemyData.enemyHealth}");
        if (enemyData.enemyHealth <= 0)
        {
            Die();
            GameManagerScript.Instance.enemyAmount--;
            Debug.Log($"{gameObject.name} has met their demise");
        }
    }

    // Zorgt ervoor dat enemies met de speler kunnen colliden
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger is triggeredeth");
        if (collision.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            Debug.Log(gameObject.name + " hit " + collision.gameObject.name);

            // Mits de speler de IDamagable interface heeft dan pakt hij de TakeDamage functie
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(enemyData.enemyDamage);
        }
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
