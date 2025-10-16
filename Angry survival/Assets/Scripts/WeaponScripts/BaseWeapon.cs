using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseWeapon : MonoBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;

    // Temporary variable
    public InputAction upgradeInput;

    // Interfaces
    private IWeaponBehaviour weaponBehaviour;

    // Basic variables
    public int weaponLevel, maxLevel, durability;
    public float cooldown, trueCooldown, damage;
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
    public void FixedUpdate()
    {
        if (upgradeInput.ReadValue<float>() == 1)
        {
            Debug.Log("Upgrade input detected");
            Upgrade();
        }
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

    public void OnEnable()
    {
        upgradeInput.Enable();
    }
    public void OnDisable()
    {
        upgradeInput.Disable();
    }
}
