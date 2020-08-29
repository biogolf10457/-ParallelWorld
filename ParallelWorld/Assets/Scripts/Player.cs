using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedY;
    public float speedX;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;
    public KeyCode left;
    public KeyCode right;
    public KeyCode useItem;
    private float realSpeedX;
    private float realSpeedY;
    private string items;
    public GameObject rival;
    private Player rivalScript;
    private float slowDuration;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        realSpeedY = speedY;
        items = "";
        rivalScript = rival.GetComponent<Player>();
        slowDuration = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease time duration
        if(slowDuration > 0)
        {
            UnityEngine.Debug.Log(slowDuration);
            slowDuration -= Time.deltaTime;
        }
        else
        {
            slowDuration = 0.0f;
            setDefault();
        }

        //use item
        if (Input.GetKey(useItem) && string.Compare(items, "") != 0)
        {
            if(string.Compare(items, "frog") == 0)
            {
                rivalScript.slow(2.00f);
            }
            if (string.Compare(items, "shield") == 0)
            {
                //rival.slow();
            }
            items = "";
        }

        //control movement
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
        rb.velocity = new Vector2(realSpeedX, realSpeedY * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            randomItem();
        }
    }

    private void randomItem()
    {
        if (this.transform.position.y > rival.transform.position.y)
        {
            items = "shield";
        }
        else
        {
            items = "frog";
        }
        UnityEngine.Debug.Log(items);
    }

    private void setDefault()
    {
        realSpeedY = speedY;
    }

    public void slow(float duration)
    {
        realSpeedY = realSpeedY / 2;
        slowDuration = duration;
    }
}
