using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
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
    private float immunityDuration;
    private float blindDuration;
    private float trapDuration;
    public GameObject immunity;
    public GameObject blind;
    public Text itemText;
    private float initX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        realSpeedY = speedY;
        items = "";
        rivalScript = rival.GetComponent<Player>();
        slowDuration = 0.0f;
        initX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease time duration
        //slow
        if (slowDuration > 0)
        {
            slowDuration -= Time.deltaTime;
        }
        else
        {
            slowDuration = 0.0f;
            setDefault();
        }
        //immunity
        if (immunityDuration > 0)
        {
            immunity.SetActive(true);
            immunityDuration -= Time.deltaTime;
        }
        else
        {
            immunity.SetActive(false);
            immunityDuration = 0.0f;
        }
        //blind
        if (blindDuration > 0)
        {
            blind.SetActive(true);
            blind.transform.position = new Vector3(initX, transform.position.y + 3, transform.position.z);
            blindDuration -= Time.deltaTime;
        }
        else
        {
            blind.SetActive(false);
            blindDuration = 0.0f;
        }
        //trap
        if (trapDuration > 0)
        {
            trapDuration -= Time.deltaTime;
        }
        else
        {
            trapDuration = 0.0f;
            setDefault();
        }

        //use item
        if (Input.GetKey(useItem) && !string.Equals(items, ""))
        {
            if (string.Equals(items, "immunity"))
            {
                this.createImmunity(5.00f);
            }
            if (string.Equals(items, "blocker"))
            {
                //do something
            }
            if (string.Equals(items, "blind"))
            {
                rivalScript.blindVision(4.00f);
            }
            if (string.Equals(items, "slow"))
            {
                rivalScript.slow(3.00f);
            }
            if (string.Equals(items, "trap"))
            {
                rivalScript.trap(2.00f);
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
        if (string.Equals(itemName,"swap"))
        {
            float tmpX = rival.transform.position.x - rivalScript.initX;
            float tmpY = rival.transform.position.y;
            //change rival
            rival.transform.position = new Vector3(rivalScript.initX + (transform.position.x - initX), transform.position.y, rival.transform.position.z);
            //change myself
            transform.position = new Vector3(initX + tmpX, tmpY, transform.position.z);

            rivalScript.trap(2.00f);
            this.trap(2.00f);
        }
        else
        {
            items = itemName;
        }
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
        if(immunityDuration == 0)
        {
            realSpeedY = realSpeedY / 2;
            slowDuration = duration;
        }
    }
    public void blindVision(float duration)
    {
        if (immunityDuration == 0)
        {
            blindDuration = duration;
        }
    }
    public void trap(float duration)
    {
        if (immunityDuration == 0)
        {
            realSpeedY = 0;
            trapDuration = duration;
        }
    }

    public void createImmunity(float duration)
    {
        immunityDuration = duration;
    }
     
}
