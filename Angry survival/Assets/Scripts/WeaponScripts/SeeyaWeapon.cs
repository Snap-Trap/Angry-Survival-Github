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
    }
    public void Attack()
    {
        var tempBullet = Instantiate(weaponData.projectile, firePoint.position, Quaternion.identity);
        var proj = tempBullet.GetComponent<ProjectileScript>();
        if (proj != null)
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);

        Destroy(tempBullet, 5f);
    }

    public void UpgradeWeapon()
    {
        int level = baseWeapon.GetWeaponLevel();

        if (level == 1)
        {
            return;
        }
    }


}
