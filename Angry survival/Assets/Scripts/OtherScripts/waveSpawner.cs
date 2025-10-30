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
    public List<GameObject> enemyPool1, enemyPool2, enemyPool3, enemyPool4, enemyPool5, enemyPool6, enemyPool7, enemyPool8, enemyPool9, enemyPool10, enemyPool11, enemyPoll12, enemyPoll13, enemyPoll14 = new List<GameObject>();
    private List<GameObject> activePool = new List<GameObject>();

    // Variables for special wave modifications

    private float originalBaseHealth, originalSpawnInterval, originalBaseSpeed;
    // private WaveModifier currentModifier = WaveModifier.None;

    // Basic variables

    public float maxWaveTime, spawnInterval, playerRadius, eliteChance, specialWaveChance;

    private float spawnTimer, waveUpgradeTimer;

    public int waveLevel, maxEnemyAmount;

    private bool pauseWave = false;

    public Transform spawnLocation;

    public Image waveBarFill;

    public GameObject eliteEnemyPrefab;

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

        int enemiesBurstSpawned = Random.Range(1, Mathf.Clamp(waveLevel / 2, 2, 5));


        for (int i = 0; i < enemiesBurstSpawned; i++)
        {

            // Spawnt enemies rondom de speler
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            Vector2 spawnPos = new Vector2(transform.position.x * x, transform.position.y * y);
            spawnPos = spawnPos.normalized * playerRadius + (Vector2)spawnLocation.position;

            // Het daadwerkelijke spawn gedeelde
            bool spawnElite = Random.value < eliteChance && eliteEnemyPrefab != null;

            GameObject prefabToSpawn = spawnElite ? eliteEnemyPrefab : activePool[Random.Range(0, activePool.Count)];

            int randIndex = Random.Range(0, activePool.Count);
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            GameManagerScript.Instance.enemyAmount++;

            spawnTimer = spawnInterval + Random.Range(-0.5f, 0.5f);
        }
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
            // SpecialWaveModification();
        }

        UpdatePoolState();
    }

    //public enum WaveModifier
    //{
    //    // So apparently this is for a sort of different yet similar state machine
    //    None,
    //    TankWave,
    //    FastWave,
    //    SpeedWave,
    //    EliteWave
    //}

    //private void SpecialWaveModification()
    //{
    //    if (originalBaseHealth == 0)
    //    {
    //        originalBaseHealth = enemyData.enemyHealth;
    //    }

    //    if (originalSpawnInterval == 0)
    //    {
    //        originalSpawnInterval = spawnInterval;
    //    }

    //    if (originalBaseSpeed == 0)
    //    {
    //        originalBaseSpeed = enemyData.enemySpeed;
    //    }

    //    // So that the default values are restored
    //    enemyData.enemyHealth = originalBaseHealth;
    //    enemyData.enemySpeed = originalBaseSpeed;
    //    spawnInterval = originalSpawnInterval;
    //    currentModifier = WaveModifier.None;
    //    eliteChance = Mathf.Clamp(eliteChance - 0.15f, 0f, 1f);


    //    // Special wave modifications
    //    if (Random.value <= specialWaveChance)
    //    {
    //        currentModifier = (WaveModifier)Random.Range(1, 4);
    //        Debug.Log("Tis a special wave today");
    //    }
    //    else
    //    {
    //        Debug.Log("Just a normal wave today");
    //    }

    //    switch (currentModifier)
    //    {
    //        case WaveModifier.TankWave:
    //            enemyData.enemyHealth *= 1.5f;
    //            Debug.Log("Tank wave incoming!");
    //            break;

    //        case WaveModifier.FastWave:
    //            spawnInterval = Mathf.Max(0.8f, spawnInterval * 0.6f);
    //            Debug.Log("Fast wave incoming!");
    //            break;
    //        case WaveModifier.SpeedWave:
    //            enemyData.enemySpeed *= 1.15f;
    //            Debug.Log("Speed wave incoming!");
    //            break;

    //        case WaveModifier.EliteWave:
    //            eliteChance = Mathf.Clamp(eliteChance + 0.15f, 0f, 1f);
    //            Debug.Log("Elite wave incoming!");
    //            break;
    //    }
    //}


    private void UpdatePoolState()
    {
        // State machines my beloved
        if (waveLevel < 4)
        {
            SetActivePool(1);
        }
        else if (waveLevel < 7)
        {
            SetActivePool(2);
        }
        else if (waveLevel < 10)
        {
            SetActivePool(3);
        }
        else if (waveLevel < 13)
        {
            SetActivePool(4);
        }
        else if (waveLevel < 15)
        {
            SetActivePool(5);
        }
        else if (waveLevel < 20)
        {
            SetActivePool(6);
        }
        else if (waveLevel < 22)
        {
            SetActivePool(7);
        }
        else if (waveLevel < 24)
        {
            SetActivePool(8);
        }
        else if (waveLevel < 28)
        {
            SetActivePool(9);
        }
        else if (waveLevel < 29)
        {
            SetActivePool(10);
        }
        else if (waveLevel < 30)
        {
            SetActivePool(11);
        }
        else if (waveLevel < 32)
        {
            SetActivePool(12);
        }
        else if (waveLevel < 35)
        {
            SetActivePool(13);
        }
        else
        {
            SetActivePool(14);
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
            case 4:
                activePool = enemyPool4;
                break;
            case 5:
                activePool = enemyPool5;
                break;
            case 6:
                activePool = enemyPool6;
                break;
            case 7:
                activePool = enemyPool7;
                break;
            case 8:
                activePool = enemyPool8;
                break;
            case 9:
                activePool = enemyPool9;
                break;
            case 10:
                activePool = enemyPool10;
                break;
            case 11:
                activePool = enemyPool11;
                break;
            case 12:
                activePool = enemyPoll12;
                break;
            case 13:
                activePool = enemyPoll13;
                break;
            case 14:
                activePool = enemyPoll14;
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