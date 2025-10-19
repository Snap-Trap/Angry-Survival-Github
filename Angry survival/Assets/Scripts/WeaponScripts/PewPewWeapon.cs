using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PewPewWeapon : MonoBehaviour, IWeaponBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    // Temporary variable
    public InputAction upgradeInput;

    // Interfaces
    private IWeaponBehaviour weaponBehaviour;

    // Basic variables
    public Transform firePoint;
    public float bulletSpeed;



    public void Awake()
    {
        firePoint = transform.parent.Find("PewFirePoint");
    }

    public void Attack()
    {
        var gm = GameManagerScript.Instance;

        firePoint.up = gm.facingDirection;

        Debug.Log("Pew Pew! The fuck else am I supposed to say?");

        var tempBullet = Instantiate(weaponData.projectile, firePoint.position, firePoint.rotation);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        var proj = tempBullet.GetComponent<ProjectileScript>();
        if (proj != null)
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);

        Destroy(tempBullet, 2f);
    }

    public void Initialize(WeaponSO weaponData, BaseWeapon baseWeapon)
    {
        this.weaponData = weaponData;
        this.baseWeapon = baseWeapon;
    }

    public void UpgradeWeapon()
    {
        int level = baseWeapon.GetWeaponLevel();

        if (level == 1)
        {
            return;
        }
        //else if (level == 2)
        //{
        //    baseWeapon.damage += 10;
        //}
        //else if (level == 3)
        //{
        //    baseWeapon.trueCooldown -= 1.5f;
        //}
        //else if (level == 4)
        //{
        //    baseWeapon.durability += 1;
        //}
        //else if (level == 5)
        //{
        //    baseWeapon.damage += 15;
        //}
        //else if (level == 7)
        //{
        //    baseWeapon.trueCooldown -= 1.5f;
        //}
        //else if (level == 8)
        //{
        //    baseWeapon.durability += 2;
        //}
        //else if (level == 9)
        //{
        //    baseWeapon.damage += 20;
        //}
    }
}
