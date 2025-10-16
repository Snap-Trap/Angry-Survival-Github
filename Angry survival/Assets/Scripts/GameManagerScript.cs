using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    // Zorgt ervoor dat de GameManager script overal toegankelijk is
    public static GameManagerScript Instance;

    // Voor de TextMashPro

    public TextMeshProUGUI scoreText, maxScoreText;

    // Basic variables
    public int enemyAmount, enemyKills, maxScore;
    public bool isFacingRight;

    private void Awake()
    {
        Instance = this;
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
        scoreText.text = "Score: " + enemyKills;
        maxScoreText.text = "Max Score: " + maxScore;
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
        UpdateUI();
    }
}
