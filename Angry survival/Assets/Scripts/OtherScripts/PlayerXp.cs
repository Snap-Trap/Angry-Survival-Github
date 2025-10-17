using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    public int currentXp, nextLevelXp, level;

    public void Start()
    {
        currentXp = 0;
        nextLevelXp = 5;
        level = 1;
    }

    public void AddXp(int xp)
    {
        currentXp += xp;
        Debug.Log($"You got {xp} and now have {currentXp}/{nextLevelXp}");
        if (currentXp >= nextLevelXp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        currentXp -= nextLevelXp;
        Debug.Log("You got to level " + level + " ,goodjob");
    }
}
