using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BulletCollision : MonoBehaviour
{
    public GameObject losingText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collided with an NPC
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Bullet collided with NPC");
            Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("Bullet collided with " + other.name);
        }

        // Destroy the bullet after it hits anything
        if (other.name != "PlayerObject")
        {
            Destroy(gameObject);
        }
    }
}
