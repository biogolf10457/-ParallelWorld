﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObstacle : MonoBehaviour
{
    public float speed;
    public int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            UnityEngine.Debug.Log("yes");
            direction *= -1;
        }
    }
}
