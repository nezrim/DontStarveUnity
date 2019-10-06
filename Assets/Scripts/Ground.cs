using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.onGround = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.onGround = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        player.onGround = false;
    }
}
