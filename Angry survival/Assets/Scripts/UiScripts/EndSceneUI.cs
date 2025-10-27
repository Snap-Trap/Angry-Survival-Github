using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class EndSceneUI : MonoBehaviour
{
    public TextMeshProUGUI endScoreText;

    public void Awake()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        endScoreText.text = $"You have slayed a total of {GameManagerScript.Instance.enemyKills} fools. Your highest was a total of {maxScore}";
    }
}
