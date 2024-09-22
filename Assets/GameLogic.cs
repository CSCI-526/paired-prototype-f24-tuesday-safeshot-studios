using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    // public PlayerController playerController;
    // public BulletManager bulletManager; 
    // public GameObject winUI;
    // public GameObject failureUI;
    public Text winText;
    public Text failureText;
    public ExitDoor exitDoor;
    private bool hasWon = false; 


    void Start()
    {
        winText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!hasWon)
        {
            CheckGameStatus();
        }
    }

    private void CheckGameStatus()
    {
        if (exitDoor.IsPlayerInRange() && bulletManager.bulletCount > 0)
        {
            WinGame();
        }
        else if (bulletManager.bulletCount == 0 || GameData.Instance.DidPlayerShootFriend)
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        winText.gameObject.SetActive(true);
        Time.timeScale = 0;
        hasWon = true;
    }

    public void LoseGame()
    {
        failureText.gameObject.SetActive(true);
        Time.timeScale = 0; 
        playerController.DisableControl();
        Debug.Log("Game Over!");
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        winText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);
        playerController.EnableControl();
        bulletManager.ResetBullets();
        hasWon = false;
        GameData.Instance.ResetData();
    }

}

public class GameData
{
    public static GameData Instance = new GameData();
    public static GameData Instance => instance ??= new GameData();

    private bool didPlayerShootFriend;
    public bool DidPlayerShootFriend
    {
        get => didPlayerShootFriend;
        set => didPlayerShootFriend = value;
    }

    private int bulletCount = 4;
    public int BulletCount
    {
        get => bulletCount;
        set => bulletCount = value;
    }

    public void ResetData()
    {
        didPlayerShootFriend = false;
        bulletCount = 4;
    }
}