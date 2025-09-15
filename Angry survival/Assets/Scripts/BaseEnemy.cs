using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IDamagable, IMovable
{
    public EnemySO enemyData;
    private IEnemyBehaviour enemyBehaviour;

    public bool canMove = true;

    public void Awake()
    {
        

        if (TryGetComponent<IEnemyBehaviour>(out enemyBehaviour))
        {
            enemyBehaviour.Initialize(enemyData);
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

    public void Movable(bool value)
    {
        canMove = true;
    }
}
