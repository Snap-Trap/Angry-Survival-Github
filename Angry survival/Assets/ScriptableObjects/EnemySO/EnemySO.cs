using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 2)]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    public string enemyName;
    public float enemyDamage;
    public float enemyHealth;
    public float enemySpeed;
    public int xp;
    public float dropRatio;
    public int goldAmount;
    public int spawnCost;
}
