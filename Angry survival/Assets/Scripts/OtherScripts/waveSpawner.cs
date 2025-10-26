using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // Scriptable object voor de enemy data
    public EnemySO enemyData;

    // Makes a list of all the possible enemy prefabs
    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();

    // Basic variables
    [SerializeField] private float waveUpgradeTimer, playerRadius;

    public Image waveBarFill;
    
    public float spawnTimer, spawnInterval, maxWaveTime;

    public int waveLevel, maxEnemyAmount, randPrefab;

    public Transform spawnLocation;

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

        maxWaveTime = 30f;
        waveLevel = 1;
        maxEnemyAmount = 15;
        waveUpgradeTimer = maxWaveTime;
    }

    // WE ARE USING FIXED UPDATE BECAUSE I DON'T TRUST FRAMES
    public void FixedUpdate()
    {
        UpdateWaveBar();

        if (!pauseWave)
        {
            BeginWave();
        }
    }

    // Wat verwacht je anders van een functie die BeginWave heet
    public void BeginWave()
    {
        waveUpgradeTimer -= Time.fixedDeltaTime;
        spawnTimer -= Time.fixedDeltaTime;

        if (waveUpgradeTimer <= 0)
        {
            // Zorgt ervoor dat de wave upgrade naarmate de tijd
            waveLevel++;
            Debug.Log("Wave got a little spicier...");
            waveUpgradeTimer = maxWaveTime;
            maxEnemyAmount += 2;
        }
        if (spawnTimer <= 0)
        {
            Debug.Log("There are: " + GameManagerScript.Instance.enemyAmount + " enemies alive");
            // Zorgt ervoor dat enemies alleen spawnen onder de max limit
            // Reminder, de spawner spawned altijd 1 enemy boven de max limit
            if (GameManagerScript.Instance.enemyAmount <= maxEnemyAmount)
            {
                SpawnEnemies();
            }
            else
            {
                Debug.Log("You greedy batsard, the entire board is already filled with shit you shitling");
            }
        }
    }

    public void UpdateWaveBar()
    {
        waveBarFill.fillAmount = 1f - (waveUpgradeTimer / maxWaveTime);
    }
    public void SpawnEnemies()
    {
        // Spawnt enemies rondom de speler
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector2 spawnPos = new Vector2(transform.position.x * x, transform.position.y * y);
        spawnPos = spawnPos.normalized * playerRadius + (Vector2)spawnLocation.position;

        // Het daadwerkelijke spawn gedeelde
        randPrefab = Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[randPrefab], spawnPos, Quaternion.identity);
        GameManagerScript.Instance.enemyAmount++;
        Debug.Log("Something moderately wicked has cometh");
        spawnTimer = spawnInterval;
    }
}