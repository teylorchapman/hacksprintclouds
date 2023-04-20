using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 desiredvelocity = Vector2.zero;
    Rigidbody2D rb;
    public float acceleration = 1f;
    public float jumpheight = 2f;
    public float gravity = 0.5f;

    bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredvelocity = (Vector2.right * Input.GetAxis("Horizontal"));
        
        Vector2 velocity = rb.velocity;
        Vector2 change = desiredvelocity - velocity;
        if(change.magnitude > acceleration)
        {
            change = change.normalized * acceleration;
        }
        if (!grounded)
            change += Vector2.down * gravity;
        else if (Input.GetButtonDown("Jump"))
        {
            change += Vector2.up * gravity * jumpheight;
            //grounded = false;
        }
        rb.velocity += change;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        checkGrounded(collision);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        checkGrounded(collision);
    }

    void checkGrounded(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.8){
            grounded = true;
        }
    }
}
