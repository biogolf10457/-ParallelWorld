using System.Collections;
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
        itemList = new string[]{"shield","potion","vision","swap","hex","trap","power","amulet","counter","random"};
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
                int randomNum = Random.Range(0, 100);
                if (randomNum >= 0 && randomNum < 10)
                {
                    child.GetComponent<Item>().setName(itemList[0]); //sheild
                }
                else if (randomNum >= 10 && randomNum < 20)
                {
                    child.GetComponent<Item>().setName(itemList[1]); //potion
                }
                else if (randomNum >= 20 && randomNum < 30)
                {
                    child.GetComponent<Item>().setName(itemList[2]); //vision
                }
                else if (randomNum >= 30 && randomNum < 33)
                {
                    child.GetComponent<Item>().setName(itemList[3]); //swap
                }
                else if (randomNum >= 33 && randomNum < 43)
                {
                    child.GetComponent<Item>().setName(itemList[4]); //hex
                }
                else if (randomNum >= 43 && randomNum < 53)
                {
                    child.GetComponent<Item>().setName(itemList[5]); //trap
                }
                else if (randomNum >= 53 && randomNum < 63)
                {
                    child.GetComponent<Item>().setName(itemList[6]); //power
                }
                else if (randomNum >= 63 && randomNum < 73)
                {
                    child.GetComponent<Item>().setName(itemList[7]); //amulet
                }
                else if (randomNum >= 73 && randomNum < 83)
                {
                    child.GetComponent<Item>().setName(itemList[8]); //counter
                }
                else if (randomNum >= 83 && randomNum < 101)
                {
                    child.GetComponent<Item>().setName(itemList[9]); //random
                }

                
                if (player.transform.position.y > player.GetComponent<Player>().rival.transform.position.y) //condition for 1st runner
                {
                    if (randomNum >= 63 && randomNum < 73) //get amulet
                    {
                        child.GetComponent<Item>().setName(itemList[9]);
                    }
                }
                else //condition for 2nd runner
                {
                    if (randomNum >= 43 && randomNum < 53) //get trap
                    {
                        child.GetComponent<Item>().setName(itemList[9]);
                    }
                }
            }
        }
    }
}
