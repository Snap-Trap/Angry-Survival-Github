using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 2)]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab, xpPrefab, xpPrefab2;
    public string enemyName;
    public float enemyDamage;
    public float enemyHealth;
    public float enemySpeed;
    public float dropRatio, dropRatio2;
}
