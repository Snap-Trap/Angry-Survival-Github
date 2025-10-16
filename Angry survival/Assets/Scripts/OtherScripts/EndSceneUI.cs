using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneUI : MonoBehaviour
{
    public TextMeshProUGUI endScoreText;

    void Start()
    {
        int finalScore = GameManagerScript.Instance.enemyKills;
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        endScoreText.text = $"You have slayed a total of {finalScore} fools. Your highest was a total of {maxScore}";
    }
}
