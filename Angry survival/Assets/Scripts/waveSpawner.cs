using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    // Scriptable object voor de enemy data
    public EnemySO enemyData;

    // Makes a list of the waves
    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform spawnLocation;

    // Basic variables
    [SerializeField] private float waveUpgradeTimer, playerRadius;
    
    public float spawnTimer, spawnInterval;

    public int waveLevel, maxEnemyAmount, enemyAmount;

    public bool pauseWave;

    // Code stuff
    public void Initialize(EnemySO enemyData)
    {
        this.enemyData = enemyData;
    }

    private void Start()
    {
        BeginWave();

        spawnLocation = GameObject.FindGameObjectWithTag("Player").transform;

        waveLevel = 1;
        maxEnemyAmount = 10;
        waveUpgradeTimer = 10f;
    }

    // WE ARE USING FIXED UPDATE BECAUSE I DON'T TRUST FRAMES
    public void FixedUpdate()
    {
        if (!pauseWave)
        {
            BeginWave();
        }
    }

    public void BeginWave()
    {
        if (waveUpgradeTimer <= 0)
        {
            waveLevel++;
            Debug.Log("Wave got a little spicier...");
            waveUpgradeTimer = 10f;
        }
        else
        {
            waveUpgradeTimer -= Time.fixedDeltaTime;
        }

        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count <= maxEnemyAmount)
            {
                SpawnEnemies();
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
    }

    public void SpawnEnemies()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector2 spawnPos = new Vector2(transform.position.x * x, transform.position.y * y);
        spawnPos = spawnPos.normalized * playerRadius + (Vector2)spawnLocation.position;
        // Makes the first enemy in the list spawn because index is 0, why? Dunno.
        Instantiate(enemiesToSpawn[0], spawnPos, Quaternion.identity);

        // If the spawn timer is zero then the spawninterval is also 0
        spawnTimer = spawnInterval;
    }
}