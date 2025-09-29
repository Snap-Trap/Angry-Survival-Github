using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IDamagable, IMovable
{
    // Scriptable object voor de enemy data
    public EnemySO enemyData;

    // Main interface die elke andere enemy gaat gebruiken
    private IEnemyBehaviour enemyBehaviour;

    // Standaard variables
    public bool canMove, canInteractPlayer;
    public float health;

    // Layermask

    public LayerMask playerLayer;

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

    public void Update()
    {
        enemyBehaviour.Movement();
    }
    public void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        enemyData.enemyHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {enemyData.enemyHealth}");
        if (enemyData.enemyHealth <= 0)
        {
            Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger is triggeredeth");
        if (collision.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            Debug.Log(gameObject.name + " hit " + collision.gameObject.name);
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(enemyData.enemyDamage);
        }
    }

    private void CreateEnemy()
    {
        gameObject.name = enemyData.enemyName;

        health = enemyData.enemyHealth;
    }

    public void Movable(bool value)
    {
        canMove = true;
    }
}
