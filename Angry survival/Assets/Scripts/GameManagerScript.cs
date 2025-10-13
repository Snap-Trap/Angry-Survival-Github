using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public int enemyAmount = 0;
    public bool isFacingRight;

    private void Awake()
    {
        Instance = this;
    }
}
