using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public List<string> weaponDescriptionList = new List<string>();
    public Sprite weaponIcon;
    public string weaponName;
    public float weaponDamage;
    public float weaponCooldown;
    public int weaponDurability;
    public GameObject projectile;
}
