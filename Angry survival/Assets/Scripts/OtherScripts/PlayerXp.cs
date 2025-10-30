using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXp : MonoBehaviour
{
    public int currentXp, nextLevelXp, level;

    public Image xpBarFill;

    public LevelUpUIManager levelUpUI;

    public void Start()
    {
        levelUpUI = FindObjectOfType<LevelUpUIManager>();

        currentXp = 0;
        nextLevelXp = 3;
        level = 1;
    }

    public void UpdateXpbar()
    {
        xpBarFill.fillAmount = currentXp / (float)nextLevelXp;
    }

    public void AddXp(int xp)
    {
        currentXp += xp;

        Debug.Log($"You got {xp} and now have {currentXp}/{nextLevelXp}");
        if (currentXp >= nextLevelXp)
        {
            LevelUp();
        }

        if (xpBarFill != null)
        {
            UpdateXpbar();
        }
    }

    public void LevelUp()
    {
        level++;
        currentXp -= nextLevelXp;
        nextLevelXp += 5;

        Debug.Log("You got to level " + level + " ,goodjob");

        levelUpUI.ShowLevelUpChoices();
    }
}
