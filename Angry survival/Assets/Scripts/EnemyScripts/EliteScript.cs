using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteScript : MonoBehaviour, IEnemyBehaviour
{
    public EnemySO enemyData;
    public bool canMove = true;
    public Transform player;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    public void Initialize(EnemySO enemyData)
    {
        this.enemyData = enemyData;
    }
    public void Movement()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.enemySpeed * Time.deltaTime);
        }
    }
}
