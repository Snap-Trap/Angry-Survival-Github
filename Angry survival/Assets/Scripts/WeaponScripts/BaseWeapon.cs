using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseWeapon : MonoBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;

    // Interfaces
    private IWeaponBehaviour weaponBehaviour;

    // Basic variables
    public int weaponLevel, maxLevel;
    public float cooldown;
    public void Awake()
    {
        weaponBehaviour = GetComponent<IWeaponBehaviour>();

        // ? zorgt ervoor dat je checkt of de reference gelinkt is
        weaponBehaviour?.Initialize(weaponData, this);
        cooldown = weaponData.weaponCooldown;
    }
    public void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;

        if (cooldown <= 0)
        {
            weaponBehaviour?.Attack();
            cooldown = weaponData.weaponCooldown;
        }
    }

    public bool CanUpgrade() => weaponLevel < maxLevel;

    public int GetWeaponLevel() => weaponLevel;
 
    public void Upgrade()
    {
        if (!CanUpgrade()) return;

        weaponLevel++;
        Debug.Log("Upgraded weapon to level " + weaponLevel);
    }
}
