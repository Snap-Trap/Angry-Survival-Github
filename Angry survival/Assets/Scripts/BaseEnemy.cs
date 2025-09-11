using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IDamagable, IMove
{
    public EnemySO enemyData;
    public Transform player;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public void Update()
    {
        Movement(enemyData.enemySpeed);
    }

    public void Movement(float speed)
    {
        if (player.position.x > transform.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (player.position.x < transform.position.x)
        {
            transform.Translate(Vector2.right * -speed * Time.deltaTime);
        }
        if (player.position.y > transform.position.y)
        {
            transform.Translate(Vector2.down * -speed * Time.deltaTime);
        }
        else if (player.position.y < transform.position.y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
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

    public void Die()
    {
        Destroy(gameObject);
    }

}
