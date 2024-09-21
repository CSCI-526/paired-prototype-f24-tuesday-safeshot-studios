using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an NPC
        if (other.CompareTag("NPC")){
            Debug.Log("Bullet collided with NPC");
        }else{
            Debug.Log("Bullet collided with something else");
        }

        // Destroy the bullet after it hits anything
        Destroy(gameObject);
    }
}
