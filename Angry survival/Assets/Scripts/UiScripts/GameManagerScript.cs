using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Zorgt ervoor dat de GameManager script overal toegankelijk is
    // Static zodat het overal toegankelijk is, INCLUDING andere scenes
    public static GameManagerScript Instance;

    // Voor de TextMashPro
    public TextMeshProUGUI scoreText, maxScoreText;
    public static TextMeshProUGUI endScore;

    // Basic variables
    public int enemyKills;
    public int enemyAmount, maxScore;

    public Vector2 facingDirection = Vector2.right;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Calls the update function at the start to make sure it's fucking updated to 0
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        UpdateUI();
    }


    private void UpdateUI()
    {
        // Pretty much the "update" like the name implies
        if (scoreText != null)
        {
            scoreText.text = "Score: " + enemyKills;
        }

        if (maxScoreText != null)
        {
            maxScoreText.text = "Max Score: " + maxScore;
        }
    }

    public void AddKill()
    {
        enemyKills++;
        CheckMaxScore();
        UpdateUI();
    }

    private void CheckMaxScore()
    {
        // Checks so that the enemyKills won't be higher than what's displayed
        if (enemyKills > maxScore)
        {
            maxScore = enemyKills;

            // Via PlayerPrefs I make sure that the max score is saved
            PlayerPrefs.SetInt("MaxScore", maxScore);
            PlayerPrefs.Save();
        }
    }
    public void ShowEndScore()
    {
        // My brother in retardation, this is where the end score is shown, like the fucking name implies
        if (endScore != null)
        {
            endScore.text = "You have slayed a total of " + enemyKills + " fools. Your highest was a total of " + maxScore;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TheLevel")
        {
            scoreText = GameObject.Find("CurrentScore")?.GetComponent<TextMeshProUGUI>();
            maxScoreText = GameObject.Find("MaxScore")?.GetComponent<TextMeshProUGUI>();
        }
        // Nodig voor wanneer je restart anders updaten de variablen niet

        UpdateUI();
    }

    public void ResetScore()
    {
        // What do you think?
        enemyKills = 0;
        UpdateUI();
    }

    public void ResetMaxScore()
    {
        // Deletes the max score from PlayerPrefs
        PlayerPrefs.DeleteKey("MaxScore");
        maxScore = 0;
        UpdateUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
