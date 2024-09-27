using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private GameLogic gameLogic;

    public GameObject winningText;

    private static bool gameOver = false;
    public static bool isGameOver()
    {
        return gameOver;
    }

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!gameOver)
            {
                gameOver = true;
                Instantiate(winningText, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}