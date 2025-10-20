using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public WeaponSO weaponData;

    public int remainingDurability;

    public float damage;

    public void Initialize(int durability, float damage)
    {
        remainingDurability = durability;
        this.damage = damage;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("The trigger holder has bestowed upon thee its power");
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
        {
            Debug.Log(gameObject.name + " struck " + collision.gameObject.name);

            collision.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
            remainingDurability--;

            if (remainingDurability <= 0)
            {
                var seeya = GetComponent<SeeyaProjectileScript>();
                if (seeya != null)
                {
                    seeya.Explode();
                }

                Destroy(gameObject);
            }
        }
    }
}
