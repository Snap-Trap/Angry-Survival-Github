using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseWeapon : MonoBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;

    // Interfaces
    private IWeaponBehaviour weaponBehaviour;

    // Basic variables
    public int weaponLevel, maxLevel, durability;
    public float cooldown, trueCooldown, damage;
    
    public List<string> weaponDescriptionList = new List<string>();

    public void Awake()
    {
        weaponBehaviour = GetComponent<IWeaponBehaviour>();

        // ? zorgt ervoor dat je checkt of de reference gelinkt is
        weaponBehaviour?.Initialize(weaponData, this);

        // Zorgt ervoor dat de variablen van het wapen worden ingesteld
        trueCooldown = weaponData.weaponCooldown;
        damage = weaponData.weaponDamage;
        durability = weaponData.weaponDurability;
    }

    public void Start()
    {
        if (weaponLevel == 0)
        {
            // Disable the GameObject so its attack logic doesn’t run
            gameObject.SetActive(false);
        }
    }

    public void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;

        if (cooldown <= 0)
        {
            weaponBehaviour?.Attack();
            cooldown = trueCooldown;
        }
    }

    public bool CanUpgrade() => weaponLevel < maxLevel;

    public int GetWeaponLevel() => weaponLevel;

    public void Upgrade()
    {
        if (!CanUpgrade()) return;

        weaponLevel++;
        Debug.Log("Upgraded weapon to level " + weaponLevel);

        weaponBehaviour?.UpgradeWeapon();
    }

    public string GetWeaponDescription(int level)
    {
        int levelIndex = weaponLevel;
        return weaponData.weaponDescriptionList[levelIndex];
    }
}
