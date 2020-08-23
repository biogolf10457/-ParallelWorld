using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool IsGrounded;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounded = collision != null && collision.gameObject.CompareTag("Ground");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}

