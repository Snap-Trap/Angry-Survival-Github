using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAddScript : MonoBehaviour
{
    public int healthValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.AddHealth(healthValue);
                Destroy(gameObject);
            }
        }
    }
}
