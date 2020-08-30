using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private int childrenNum;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        childrenNum = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        childrenNum = gameObject.transform.childCount;
        for (int i = 0; i < childrenNum-1; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.transform.position.y < player.transform.position.y + 20 && !child.GetComponent<Item>().getIsCreated())
            {
                child.GetComponent<Item>().setName("frog");
            }
        }
    }
}
