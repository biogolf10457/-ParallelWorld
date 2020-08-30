using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string name;
    private Player playerScript;
    private bool isCreated;
    private string[] commonItem;

    void Start()
    {
        isCreated = false;
        commonItem = new string[] { "immunity", "blocker", "blind", "slow", "trap" };
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript = collision.GetComponent<Player>();
            if (string.Equals(playerScript.getItem(),""))
            {
                playerScript.setItem(name);
                Destroy(gameObject);
            }
        }
    }

    public bool getIsCreated()
    {
        return isCreated;
    }

    public void setName(string itemName)
    {
        name = itemName;
        isCreated = true;

        if(string.Equals(itemName, "random"))
        {
            int rnd = Random.Range(0, commonItem.Length-1);
            name = commonItem[rnd];
        }
    }
}
