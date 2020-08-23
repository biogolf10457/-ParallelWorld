using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public GameObject Player;
    public float adjustY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + adjustY, -10);
        }
    }
}
