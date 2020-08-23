using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedY;
    public float speedX;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;
    public KeyCode left;
    public KeyCode right;
    public float realSpeedX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
        {
            realSpeedX = -speedX * Time.deltaTime;
        }
        else if (Input.GetKey(right))
        {
            realSpeedX = speedX * Time.deltaTime;
        }
        else
        {
            realSpeedX = 0;
        }
        rb.velocity = new Vector2(realSpeedX, speedY * Time.deltaTime);
    }
}
