using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public WeaponSO weaponData;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("The trigger holder has bestowed upon thee its power");
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
        {
            Debug.Log(gameObject.name + " struck " + collision.gameObject.name);

            collision.gameObject.GetComponent<IDamagable>().TakeDamage(weaponData.weaponDamage);
        }
    }
}
