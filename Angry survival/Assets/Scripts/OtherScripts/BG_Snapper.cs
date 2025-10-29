using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BG_Snapper : MonoBehaviour
{
    private float width;
    private float height;

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - transform.position.x > width/2)
        {
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x - transform.position.x < -width/2)
        {
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
        }

        if (player.transform.position.y - transform.position.y > height / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }
        else if (player.transform.position.y - transform.position.y < -height / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
        }
    }
}
