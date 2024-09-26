using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class movePlayer : MonoBehaviour
{
    public Camera camera;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    private int bulletCount = 0;

    public TextMeshProUGUI bulletCountText;

    public int bulletLimit = 10;

    public Transform obj;

    public Transform gun;

    public GameObject losingText;
    public float force = 5f;

    Vector2 playerPos;
    Vector2 mousePos;
    float mousePosY, mousePosX;

    private Rigidbody2D rb;
    private Vector3 direction;

    private bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        if (BulletCollision.isGameOver())
        {
            Debug.Log("Game Over for bullet collision");
            gameOver = true;
        }
        if (ExitDoor.isGameOver())
        {
            Debug.Log("Game Over for exit door");
            gameOver = true;
        }
        
        if (bulletCount >= bulletLimit && rb.velocity.magnitude == 0)
        {
            Debug.Log("Game Over as you have no bullets left");
            gameOver = true;
            Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount < bulletLimit)
            {
                playerPos = obj.transform.position;//gets player position
                mousePos = Input.mousePosition;//gets mouse postion
                mousePos = camera.ScreenToWorldPoint(mousePos);
                mousePosX = mousePos.x - playerPos.x;//gets the distance between object and mouse position for x
                mousePosY = mousePos.y - playerPos.y;//gets the distance between object and mouse position for y  if you want this.
                float dirX = mousePosX / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));
                float dirY = mousePosY / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));

                Vector2 accelerationForce = new Vector2(-dirX, -dirY) * force;
                rb.AddForce(accelerationForce, ForceMode2D.Impulse);


                ShootBullet();
                bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
            }

        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void ShootBullet()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D

        // Get the player's position (where the bullet will spawn)
        Vector3 playerPosition = transform.position;

        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);

        // Calculate the directionion
        Vector2 direction = (mousePosition - playerPosition).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // Pass the direction to the player's controller
        // if (playerController != null)
        // {
        //     playerController.ReceiveDirection(direction);
        // }

        // Increment the bullet count; maybe we should do this by calling a function in the game manager
        bulletCount++;
        Debug.Log("Bullet count: " + bulletCount);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2f);
    }

}
