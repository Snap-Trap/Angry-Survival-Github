using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PewPewWeapon : MonoBehaviour, IWeaponBehaviour
{
    // Scriptable object voor de weapons
    public WeaponSO weaponData;
    public BaseWeapon baseWeapon;

    // Basic variables
    public Transform firePoint;
    public float bulletSpeed;

    public void Awake()
    {
        firePoint = transform.parent.Find("PewFirePoint");
    }
    public void Initialize(WeaponSO weaponData, BaseWeapon baseWeapon)
    {
        this.weaponData = weaponData;
        this.baseWeapon = baseWeapon;

        FindObjectOfType<LevelUpUIManager>()?.RegisterWeapon(baseWeapon);
    }

    public void Attack()
    {
        // Grabs the direction the player is facing
        var gm = GameManagerScript.Instance;
        firePoint.up = gm.facingDirection;

        int level = baseWeapon.GetWeaponLevel();

        if (level < 6)
        {
            // Single shot
            SpawnBullet(firePoint.position, firePoint.rotation);
        }
        else if (level < 10)
        {
            // Double shot with some spacing
            float spacing = 0.5f;
            SpawnBullet(firePoint.position - firePoint.right * spacing / 2, firePoint.rotation);
            SpawnBullet(firePoint.position + firePoint.right * spacing / 2, firePoint.rotation);
        }
        else
        {
            // Triple shot (triangle pattern)
            float sideOffset = 0.5f;   // left/right distance
            float backOffset = 0.3f;  // how far back the side bullets are
            float forwardOffset = 0.3f; // how far forward the center bullet is

            // Center bullet slightly ahead
            SpawnBullet(firePoint.position + firePoint.up * forwardOffset, firePoint.rotation);

            // Left bullet: slightly back and to the left
            // position minus because it needs to go left
            SpawnBullet(firePoint.position - firePoint.right * sideOffset - firePoint.up * backOffset, firePoint.rotation);

            // Right bullet: slightly back and to the right
            SpawnBullet(firePoint.position + firePoint.right * sideOffset - firePoint.up * backOffset, firePoint.rotation);

            // Unregister weapon after max level reached so you can't select it on upgrade
            FindObjectOfType<LevelUpUIManager>()?.UnregisterWeapon(baseWeapon);
        }
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var tempBullet = Instantiate(weaponData.projectile, position, rotation);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        var proj = tempBullet.GetComponent<ProjectileScript>();
        if (proj != null)
            proj.Initialize(baseWeapon.durability, baseWeapon.damage);

        Destroy(tempBullet, 2f);
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
            baseWeapon.damage += 3;
        }
        else if (level == 3)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 4)
        {
            baseWeapon.damage += 3;
        }
        else if (level == 5)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        // Level 6 wordt gedaan in de Attack functie
        else if (level == 7)
        {
            baseWeapon.damage += 3;
        }
        else if (level == 8)
        {
            baseWeapon.trueCooldown -= 0.2f;
        }
        else if (level == 9)
        {
            baseWeapon.damage += 5;
        }
        // Level 10 wordt ook gedaan in de Attack functie
    }
}
