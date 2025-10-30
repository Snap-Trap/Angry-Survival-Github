using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackoWeapon : MonoBehaviour, IWeaponBehaviour
{
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    // Basic variables

    private float bulletSpeed;

    private Transform firePoint;

    private Vector2 targetPosition;

    public void Start()
    {
        bulletSpeed = 7f;
    }

    public void Awake()
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
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        float directionAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        var tempBullet = Instantiate(weaponData.projectile, firePoint.position, Quaternion.Euler(0f, 0f, directionAngle - 90f));

        tempBullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        var proj = tempBullet.GetComponent<ProjectileScript>();
        if (proj != null)
        {
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);
        }

        Destroy(tempBullet, 3f);
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
            baseWeapon.damage += 4f;
        }
        else if (level == 3)
        {
            baseWeapon.damage += 4f;
        }
        else if (level == 4)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 5)
        {
            baseWeapon.damage += 4f;
        }
        else if (level == 6)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 7)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 8)
        {
            baseWeapon.damage += 6f;
        }
        else if (level == 9)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 10)
        {
            baseWeapon.damage += 6f;
            // Unregister weapon after max level reached so you can't select it on upgrade
            FindObjectOfType<LevelUpUIManager>()?.UnregisterWeapon(baseWeapon);
        }
    }
}
