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
    public List<GameObject> enemyPool1, enemyPool2, enemyPool3 = new List<GameObject>();
    private List<GameObject> activePool = new List<GameObject>();

    // Basic variables
    
    public float maxWaveTime, spawnInterval, playerRadius;

    private float spawnTimer, waveUpgradeTimer;

    public int waveLevel, maxEnemyAmount;

    private bool pauseWave = false;

    public Transform spawnLocation;

    public Image waveBarFill;

    // Code stuff
    public void Initialize(EnemySO enemyData)
    {
        this.enemyData = enemyData;
    }

    private void Start()
    {
        spawnLocation = GameObject.FindGameObjectWithTag("Player").transform;

        spawnTimer = spawnInterval;
        waveUpgradeTimer = maxWaveTime;

        SetActivePool(1);
    }

    // WE ARE USING FIXED UPDATE BECAUSE I DON'T TRUST FRAMES
    public void FixedUpdate()
    {
        UpdateWaveBar();

        if (pauseWave)
        {
            return;
        }

        WaveTimer();
        WaveSpawning();
    }

    public void SpawnEnemies()
    {
        if (activePool == null || activePool.Count == 0)
        {
            return;
        }

        // Spawnt enemies rondom de speler
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector2 spawnPos = new Vector2(transform.position.x * x, transform.position.y * y);
        spawnPos = spawnPos.normalized * playerRadius + (Vector2)spawnLocation.position;

        // Het daadwerkelijke spawn gedeelde
        int randIndex = Random.Range(0, activePool.Count);
        Instantiate(activePool[randIndex], spawnPos, Quaternion.identity);

        GameManagerScript.Instance.enemyAmount++;
        spawnTimer = spawnInterval;
    }

    private void WaveSpawning()
    {
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

    private void WaveTimer()
    {
        waveUpgradeTimer -= Time.fixedDeltaTime;
        spawnTimer -= Time.fixedDeltaTime;

        if (waveUpgradeTimer <= 0)
        {
            // Zorgt ervoor dat de wave upgrade naarmate de tijd
            waveLevel++;
            waveUpgradeTimer = maxWaveTime;
            maxEnemyAmount += 2;

            if (spawnInterval > 1.4f)
            {
                spawnInterval -= 0.2f;
            }
            else
            {
                return;
            }
        }

        UpdatePoolState();
    }

    private void UpdatePoolState()
    {
        // State machines my beloved
        if (waveLevel < 5)
        {
            SetActivePool(1);
        }
        else if (waveLevel < 10)
        {
            SetActivePool(2);
        }
        else
        {
            SetActivePool(3);
        }
    }

    private void SetActivePool(int poolNumber)
    {
        switch (poolNumber)
        {
            case 1:
                activePool = enemyPool1;
                break;
            case 2:
                activePool = enemyPool2;
                break;
            case 3:
                activePool = enemyPool3;
                break;
            default:
                activePool = enemyPool1;
                break;
        }
    }

    public void UpdateWaveBar()
    {
        waveBarFill.fillAmount = 1f - (waveUpgradeTimer / maxWaveTime);
    }
}