using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeeyaProjectileScript : MonoBehaviour
{
    private ProjectileScript projectileScript;

    public bool canExplode = false;
    public float speed, rotateSpeed, detectionRange, explosionRadius;

    private Rigidbody2D rb;
    private Transform target;

    public void Start()
    {
        projectileScript = GetComponent<ProjectileScript>();
        rb = GetComponent<Rigidbody2D>();
        // Not going to put this in an update because it only needs to do it once this projectile is suicidal bruh
        FindNearestTarget();
    }
    void FixedUpdate()
    {
        if (target == null)
        {
            FindNearestTarget();
            if (target == null)
            {
                rb.velocity = Vector2.zero;
                return;
            }
        }

        // This (hopefully) grabs the direction towards the target by a simply equation
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // Besides the basic float you see, angular velocity will basically rotate at a certain speed in a certain timeframe, I think
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        // Nyooom, goes forward
        rb.velocity = transform.up * speed;
    }

    private void FindNearestTarget()
    {
        // DOMAIN EXPANSION: UNLIMITED VOID!!!
        float nearestDistance = Mathf.Infinity;

        // Since I have this function in the start I also always gotta make sure that the nearestEnemy is reset
        Transform nearestEnemy = null;

        // Makes a list of all the colliders inside le detection range which I made a circle
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
            {
                // Gets teh distance between the projectile and the enemy
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                // If the distance of whatever you hit last is less than the nearest distance then it becomes the new nearest distance
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = hit.transform;
                }
            }
        }

        // The fuck do you think
        target = nearestEnemy;
    }

    public void Explode()
    {
        if (!canExplode) return;

        Debug.Log("BOOM! SeeyaProjectile exploded.");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("enemyLayer"))
            {
                hit.gameObject.GetComponent<IDamagable>()?.TakeDamage(projectileScript.damage);
            }
        }
    }

    // For visualising this shit
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (canExplode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
