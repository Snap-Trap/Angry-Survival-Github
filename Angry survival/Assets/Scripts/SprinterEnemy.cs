using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterEnemy : MonoBehaviour, IEnemyBehaviour
{
    public EnemySO enemyData;

    public bool canSprint, isSprinting, canMove;
    public Transform player;
    public void Awake()
    {

        canMove = true;
        canSprint = false;
        isSprinting = false;
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
            if (!isSprinting)
            {
                StartCoroutine(SprintCooldown(2f, 5f));
            }   
        }
        if (canSprint)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.enemySpeed * 2 * Time.deltaTime);
        }
    }

    IEnumerator SprintCooldown(float sprintDuration, float sprintDowntime)
    {
        isSprinting = true;
        canSprint = true;
        yield return new WaitForSeconds(sprintDuration);
        canSprint = false;
        canMove = true;
        yield return new WaitForSeconds(sprintDowntime);
        isSprinting = false;
    }
}
