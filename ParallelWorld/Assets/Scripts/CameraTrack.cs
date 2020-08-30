using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public GameObject Player;
    private float adjustY;
    private float initX;
    private float initY;
    public GameObject[] levels;
    private Camera mainCamera;
    public Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //set start position of camera
        initX = Player.transform.position.x; 
        initY = Player.transform.position.y;
        adjustY = 4.00f;

        //set background buffer
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in levels)
        {
            loadChildObjects(obj);
        }
    }

    void loadChildObjects(GameObject obj)
    {
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
        UnityEngine.Debug.Log(screenBounds.y + " , " + objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;

        childsNeeded = 2;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y - choke;
            if (transform.position.y + screenBounds.y > lastChild.transform.position.y - halfObjectHeight)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjectHeight * 2, lastChild.transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(initX, Player.transform.position.y + adjustY, -10);
        }
    }

    void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }
}
