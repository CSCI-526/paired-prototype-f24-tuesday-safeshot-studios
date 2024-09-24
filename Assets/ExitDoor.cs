using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameLogic != null)
            {
                // gameLogic.GameOver();
            }
        }
    }
}