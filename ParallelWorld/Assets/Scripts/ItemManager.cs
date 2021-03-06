﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int childrenNum;
    public GameObject player;
    private string[] itemList;
    // Start is called before the first frame update
    void Start()
    {
        childrenNum = gameObject.transform.childCount;
        itemList = new string[]{"immunity","blocker","blind","swap","slow","trap","random"};
    }

    // Update is called once per frame
    void Update()
    {
        childrenNum = gameObject.transform.childCount;
        for (int i = 0; i < childrenNum; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.transform.position.y < player.transform.position.y + 20 && !child.GetComponent<Item>().getIsCreated())
            {
                //generate item
                int randomNum = Random.Range(0, 62);
                if (randomNum >= 0 && randomNum < 10)
                {
                    child.GetComponent<Item>().setName(itemList[0]); //immunity
                }
                else if (randomNum >= 10 && randomNum < 20)
                {
                    child.GetComponent<Item>().setName(itemList[1]); //blocker 
                }
                else if (randomNum >= 20 && randomNum < 30)
                {
                    child.GetComponent<Item>().setName(itemList[2]); //blind
                }
                else if (randomNum >= 30 && randomNum < 32)
                {
                    child.GetComponent<Item>().setName(itemList[3]); //swap
                }
                else if (randomNum >= 32 && randomNum < 42)
                {
                    child.GetComponent<Item>().setName(itemList[4]); //slow
                }
                else if (randomNum >= 42 && randomNum < 52)
                {
                    child.GetComponent<Item>().setName(itemList[5]); //trap
                }
                else if (randomNum >= 52 && randomNum < 62)
                {
                    child.GetComponent<Item>().setName(itemList[6]); //random
                }
            }
        }
    }
}
