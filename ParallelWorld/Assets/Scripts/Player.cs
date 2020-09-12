﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speedY;
    public float speedX;
    private float speedXR;
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
    public GameObject blind;
    public Text itemText;
    private float initX;
    public GameObject checkBorder;
    private float limitLenght;
    public Animator animator;
    public GameObject winkEffect;
    public GameObject light;
    public GameObject dark;
    private GameObject darkEffect;
    public GameObject slowEffect;
    private GameObject slowEffectObject;

    public Text Result;
    public Animator ResultUI;
    public Transform ResultWindow;
    private IEnumerator finishgame;
    public GameObject RestartBtn;
    public GameObject MenuBtn;

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
        limitLenght = checkBorder.transform.localScale.x / 2;

        RestartBtn.gameObject.SetActive(false);
        MenuBtn.gameObject.SetActive(false);
        finishgame = DelayGame();
    }

    // Update is called once per frame
    void Update()
    {
        //state of animation
        animator.SetFloat("realSpeedY", realSpeedY);
        animator.SetFloat("immunityDuration", immunityDuration);

        //slow
        if (slowDuration > 0)
        {
            if(slowEffectObject != null){
                slowEffectObject.transform.position = new Vector3 (transform.position.x , transform.position.y, transform.position.z);
            }
            slowDuration -= Time.deltaTime;
        }
        else
        {
            slowDuration = 0.0f;
        }
        //immunity
        if (immunityDuration > 0)
        {
            immunityDuration -= Time.deltaTime;
        }
        else
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
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
            if(darkEffect != null){
                Destroy(darkEffect);
            }
        }
        //set defualt if no debuff
        if(trapDuration <= 0 && slowDuration <= 0)
        {
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
            realSpeedX = -speedXR * Time.deltaTime;
        }
        else if (Input.GetKey(right))
        {
            realSpeedX = speedXR * Time.deltaTime;
        }
        else
        {
            realSpeedX = 0;
        }


        rb.velocity = new Vector2(realSpeedX, realSpeedY * Time.deltaTime);
        //avoid out of map
        if(transform.position.x > initX + limitLenght){
            transform.position = new Vector3(initX + limitLenght, transform.position.y, transform.position.z);
        }else if(transform.position.x < initX - limitLenght){
            transform.position = new Vector3(initX - limitLenght, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoalFinal"))
        {
            UnityEngine.Debug.Log(this.name + " win !");
            ResultWindow.gameObject.SetActive(true);
            Result.color = Color.green;
            Result.text = "WINNER";
            ResultUI.SetBool("result", true);
            StartCoroutine(finishgame);
        }
    }

        private IEnumerator DelayGame()
    {
        yield return new WaitForSeconds(3);
        RestartBtn.gameObject.SetActive(true);
        MenuBtn.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

        public void Menu() {
            Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void setItem(string itemName)
    {
        if (string.Equals(itemName,"swap"))
        {
            float tmpX = rival.transform.position.x - rivalScript.initX;
            float tmpY = rival.transform.position.y;
            //change rival
            rival.transform.position = new Vector3(rivalScript.initX, transform.position.y, rival.transform.position.z);
            //change myself
            transform.position = new Vector3(initX, tmpY, transform.position.z);

            rivalScript.trap(2.00f);
            this.trap(2.00f);
            Destroy(darkEffect);
            Instantiate(light, new Vector3(transform.position.x , transform.position.y, transform.position.z),transform.rotation);
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
        speedXR = speedX;
    }

    public void slow(float duration)
    {
        if(immunityDuration <= 0)
        {
            slowEffectObject = Instantiate(slowEffect, new Vector3(transform.position.x , transform.position.y+1, transform.position.z),transform.rotation);
            realSpeedY = realSpeedY / 2;
            speedXR = speedXR / 2;
            slowDuration = duration;
        }
    }
    public void blindVision(float duration)
    {
        if (immunityDuration <= 0)
        {
            blindDuration = duration;
        }
    }
    public void trap(float duration)
    {
        if (immunityDuration <= 0)
        {
            darkEffect = Instantiate(dark, new Vector3(transform.position.x , transform.position.y, transform.position.z),transform.rotation);
            realSpeedY = 0.00f;
            speedXR = 0.00f;
            trapDuration = duration;
        }
    }

    public void createImmunity(float duration)
    {
        Instantiate(winkEffect, new Vector3(transform.position.x , transform.position.y + 1, transform.position.z),transform.rotation);
        immunityDuration = duration;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
