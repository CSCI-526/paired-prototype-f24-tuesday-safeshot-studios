using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public Camera camera;
    public Transform obj;

    public Transform gun;


    public float force = 5f;

    Vector2 playerPos;
    Vector2 mousePos;
    float mousePosY, mousePosX;

    private Rigidbody2D rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            playerPos = obj.transform.position;//gets player position
            mousePos = Input.mousePosition;//gets mouse postion
            mousePos = camera.ScreenToWorldPoint(mousePos);
            mousePosX = mousePos.x - playerPos.x;//gets the distance between object and mouse position for x
            mousePosY = mousePos.y - playerPos.y;//gets the distance between object and mouse position for y  if you want this.
            float dirX = mousePosX / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));
            float dirY = mousePosY / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));

            Vector2 accelerationForce = new Vector2(-dirX, -dirY) * force;
            rb.AddForce(accelerationForce, ForceMode2D.Impulse);          
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
