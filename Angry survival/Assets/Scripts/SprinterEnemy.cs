using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterEnemy : MonoBehaviour, IEnemyBehaviour
{
    public EnemySO enemyData;

    public bool canMove, canSprint;
    public Transform player;
    public void Awake()
    {
        canMove = true;
        canSprint = false;
        player = GameObject.Find("Player").transform;
    }

    public void Initialize(EnemySO enemyData)
    {
        this.enemyData = enemyData;
    }

    // Simple movement for enemy
    public void Movement()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.enemySpeed * Time.deltaTime);
            StartCoroutine(SprintCooldown(2f, 5f));
        }
        else if (!canMove && canSprint)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.enemySpeed * 2 * Time.deltaTime);
        }
    }

    IEnumerator SprintCooldown(float sprintDuration, float sprintDowntime)
    {
        canMove = false;
        canSprint = true;
        yield return new WaitForSeconds(sprintDuration);
        canSprint = false;
        yield return new WaitForSeconds(sprintDowntime);
        canMove = true;
    }
}
