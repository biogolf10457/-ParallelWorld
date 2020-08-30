using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string name;
    private Player playerScript;

    void Start()
    {
        
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
}
