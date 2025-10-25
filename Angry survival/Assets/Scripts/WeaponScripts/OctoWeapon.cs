using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    private Transform firePoint;

    public int projectileCount = 4;
    public float bulletSpeed = 5f;

    void Start()
    {
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
        int level = baseWeapon.GetWeaponLevel();
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360f / projectileCount);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            var tempBullet = Instantiate(weaponData.projectile, firePoint.position, rotation);

            Rigidbody2D rb = tempBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(tempBullet.transform.up * bulletSpeed, ForceMode2D.Impulse);

            var proj = tempBullet.GetComponent<ProjectileScript>();
            if (proj != null)
            {
                proj.Initialize(baseWeapon.durability, baseWeapon.damage);
            }
            Destroy(tempBullet, 3f);
        }
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
            baseWeapon.trueCooldown -= 1f;
        }
        else if (level == 3)
        {
            baseWeapon.damage += 10f;
        }
        else if (level == 4)
        {
            baseWeapon.trueCooldown -= 1f;
            baseWeapon.durability += 1;
        }
        else if (level == 5)
        {
            projectileCount += 2;
        }
        else if (level == 6)
        {
            baseWeapon.damage += 15f;
        }
        else if (level == 7)
        {
            baseWeapon.trueCooldown -= 1f;
        }
        else if (level == 8)
        {
            projectileCount += 2;
        }
        else if (level == 9)
        {
            baseWeapon.trueCooldown -= 1f;
            baseWeapon.damage += 20f;
            baseWeapon.durability += 1;
        }
        else if (level == 10)
        {
            projectileCount += 2;
            // Unregister weapon after max level reached so you can't select it on upgrade
            FindObjectOfType<LevelUpUIManager>()?.UnregisterWeapon(baseWeapon);
        }
    }
}
