using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public EnemySO enemyData;

    // Reference to player script
    private PlayerMovement player;

    // Makes a list of the waves
    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();

    public List<BaseEnemy> enemies = new();

    public Transform spawnLocation;

    // Basic variables
    [SerializeField] private float waveDuration, waveTimer, playerRadius, enemyAmount;
    public float spawnTimer, spawnInterval;

    public int currentWave, waveValue;



    // Code stuff
    public void Initialize(EnemySO enemyData)
    {
        this.enemyData = enemyData;
    }

    private void Start()
    {
        BeginWave();

        playerRadius = 5f;
    }

    // WE ARE USING FIXED UPDATE BECAUSE I DON'T TRUST FRAMES
    public void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                SpawnEnemies();
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }   
    }

    public void BeginWave()
    {
        waveValue += currentWave * 5;
        CreateEnemies();
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void CreateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int enemyType = Random.Range(0, enemies.Count);
            int enemyCost = enemies[enemyType].enemyData.spawnCost;

            if (waveValue - enemyCost >= 0)
            {
                generatedEnemies.Add(enemies[enemyType].enemyData.enemyPrefab);
                waveValue -= enemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    public void SpawnEnemies()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector2 spawnPos = new Vector2(transform.position.x + Mathf.Sin(angle), transform.position.y + Mathf.Cos(angle));
        spawnPos = spawnPos.normalized * playerRadius + (Vector2)spawnLocation.position;
        // Makes the first enemy in the list spawn because index is 0, why? Dunno.
        Instantiate(enemiesToSpawn[0], spawnPos, Quaternion.identity);

        // When there are no enemies left for the list it will be removed
        enemiesToSpawn.RemoveAt(0);

        // If the spawn timer is zero then the spawninterval is also 0
        spawnTimer = spawnInterval;
    }
}