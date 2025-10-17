using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    // Basic variables

    private float bulletSpeed = 5f;

    public Transform firePoint;

    public void Awake()
    {
        firePoint = GameObject.Find("Player").transform;
    }
    public void Attack()
    {
        if (GameManagerScript.Instance.isFacingRight == true)
        {
            bulletSpeed = 5f;
        }
        else
        {
            bulletSpeed = -5f;
        }

        Debug.Log("FOR VALHALLAAAAAAAAA!!!!!!!!!");

        var tempBall = Instantiate(weaponData.projectile, firePoint.position, Quaternion.identity);
        tempBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0f), ForceMode2D.Impulse);

        var proj = tempBall.GetComponent<ProjectileScript>();
        if (proj != null)
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);

        Destroy(tempBall, 4f);
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
        else if (level == 2)
        {
            baseWeapon.damage += 10;
        }
        else if (level == 3)
        {
            baseWeapon.trueCooldown -= 1.5f;
        }
        else if (level == 4)
        {
            baseWeapon.durability += 1;
        }
        else if (level == 5)
        {
            baseWeapon.damage += 15;
        }
        else if (level == 7)
        {
            baseWeapon.trueCooldown -= 1.5f;
        }
        else if (level == 8)
        {
            baseWeapon.durability += 2;
        }
        else if (level == 9)
        {
            baseWeapon.damage += 20;
        }
        else if (level == 10)
        {
            ;
        }
    }
}
