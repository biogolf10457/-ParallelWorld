using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
    private float shieldDuration;
    public GameObject shield;
    public Text itemText;
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
            slowDuration -= Time.deltaTime;
        }
        else
        {
            slowDuration = 0.0f;
            setDefault();
        }

        if (shieldDuration > 0)
        {
            shield.SetActive(true);
            shieldDuration -= Time.deltaTime;
        }
        else
        {
            shield.SetActive(false);
            shieldDuration = 0.0f;
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
                this.createShield(2.00f);
            }
            items = "";
        }

        //item text
        itemText.text = items;

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
        if (collision.gameObject.CompareTag("Goal"))
        {
            UnityEngine.Debug.Log(this.name + " win !");
        }
    }

    public void setItem(string itemName)
    {
        items = itemName;
        UnityEngine.Debug.Log(items);
    }
    public string getItem()
    {
        return items;
    }

    private void setDefault()
    {
        realSpeedY = speedY;
    }

    public void slow(float duration)
    {
        if(shieldDuration == 0)
        {
            realSpeedY = realSpeedY / 2;
            slowDuration = duration;
        }
    }
    public void createShield(float duration)
    {
        shieldDuration = duration;
    }
     
}
