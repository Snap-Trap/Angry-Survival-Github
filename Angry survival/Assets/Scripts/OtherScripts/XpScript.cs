using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpScript : MonoBehaviour
{
    public int xpValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            PlayerXp playerXp = other.GetComponent<PlayerXp>();
            
            if (playerXp != null)
            {
                playerXp.AddXp(xpValue);
                Destroy(gameObject);
            }
        }
    }
}
