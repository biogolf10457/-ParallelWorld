using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float jumpForce;
    private BoxCollider2D boxCollider2d;
    public GameObject groundCheck;
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        if (IsGrounded() && Input.GetKeyUp(KeyCode.Space))
        {
            myRigidBody.AddForce(transform.up * jumpForce);
        }
    }

    private bool IsGrounded()
    {
        UnityEngine.Debug.Log(groundCheck.GetComponent<CheckGround>().IsGrounded);
        return groundCheck.GetComponent<CheckGround>().IsGrounded;
    }
}
