using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    // Basic variables

    private float bulletSpeed, projectileSize;

    private Transform firePoint;

    public void Awake()
    {
        projectileSize = 1f;
        bulletSpeed = 1.5f;
        firePoint = GameObject.Find("Player").transform;
    }
    public void Initialize(WeaponSO weaponData, BaseWeapon baseWeapon)
    {
        this.weaponData = weaponData;
        this.baseWeapon = baseWeapon;

        FindObjectOfType<LevelUpUIManager>()?.RegisterWeapon(baseWeapon);
    }

    public void Attack()
    {
        var facingDir = GameManagerScript.Instance.facingDirection;

        // Basically a new fancy way of saying if facing right (value above 0), bullet speed is positive, else negative
        bulletSpeed = facingDir.x >= 0 ? bulletSpeed : -bulletSpeed;

        var tempBall = Instantiate(weaponData.projectile, firePoint.position, Quaternion.identity);
        tempBall.transform.localScale *= projectileSize;
        tempBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 10f), ForceMode2D.Impulse);

        tempBall.GetComponent<SpriteRenderer>().flipX = bulletSpeed < 0;

        var proj = tempBall.GetComponent<ProjectileScript>();
        if (proj != null)
        {
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);
        }

        Destroy(tempBall, 4f);
    }

    public void UpgradeWeapon()
    {
        int level = baseWeapon.GetWeaponLevel();

        if (level == 1)
        {
            gameObject.SetActive(true);
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
            projectileSize += 0.5f;
        }
        else if (level == 6)
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
            baseWeapon.durability += 4;
        }
        else if (level == 10)
        {
            projectileSize += 0.5f;

            // Unregister weapon after max level reached so you can't select it on upgrade
            FindObjectOfType<LevelUpUIManager>()?.UnregisterWeapon(baseWeapon);
        }
    }
}
