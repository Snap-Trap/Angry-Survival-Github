using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float weaponDamage;
    public float attackCooldown;
    public GameObject projectile;
}
