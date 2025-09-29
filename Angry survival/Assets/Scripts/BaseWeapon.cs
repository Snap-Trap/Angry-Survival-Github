using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;

    // Interfaces
    private IWeaponBehaviour weaponBehaviour;

    // Basic variables
    public int weaponLevel, maxLevel;
    public float cooldown;
    private void Awake()
    {
        weaponBehaviour = GetComponent<IWeaponBehaviour>();

        // ? zorgt ervoor dat je checkt of de reference gelinkt is
        weaponBehaviour?.Initialize(weaponData, this);
    }

    public void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;

        if (cooldown <= 0)
        {
            weaponBehaviour?.Attack();
            cooldown = weaponData.attackCooldown;
        }
    }
}
