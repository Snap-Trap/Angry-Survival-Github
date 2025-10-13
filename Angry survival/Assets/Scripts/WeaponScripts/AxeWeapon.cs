using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

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
        Destroy(tempBall, 3f);
    }
    public void Initialize(WeaponSO weaponData, BaseWeapon baseWeapon)
    {
        this.weaponData = weaponData;
        this.baseWeapon = baseWeapon;
    }
}
