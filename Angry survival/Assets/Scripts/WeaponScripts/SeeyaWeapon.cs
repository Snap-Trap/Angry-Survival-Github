using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeeyaWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    private Transform firePoint;

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
        var tempBullet = Instantiate(weaponData.projectile, firePoint.position, Quaternion.identity);
        var proj = tempBullet.GetComponent<ProjectileScript>();
        if (proj != null)
        {
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);
        }

        var seeyaProj = tempBullet.GetComponent<SeeyaProjectileScript>();
        if (seeyaProj != null)
        {
            if (baseWeapon.GetWeaponLevel() >= 10)
            {
                seeyaProj.canExplode = true;

                // Unregister weapon after max level reached so you can't select it on upgrade
                FindObjectOfType<LevelUpUIManager>()?.UnregisterWeapon(baseWeapon);
            }
        }

        Destroy(tempBullet, 5f);
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
            baseWeapon.damage += 5f;
        }
        else if (level == 3)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 4)
        {
            baseWeapon.damage += 10f;
        }
        else if (level == 5)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 6)
        {
            baseWeapon.damage += 10f;
        }
        else if (level == 7)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 8)
        {
            baseWeapon.damage += 10f;
        }
        else if (level == 9)
        {
            baseWeapon.trueCooldown -= 0.2f;
            baseWeapon.damage += 10f;
        }
        // Level 10 wordt gedaan in de Attack functie + andere script
    }


}
