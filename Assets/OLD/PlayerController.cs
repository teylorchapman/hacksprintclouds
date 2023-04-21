using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 desiredvelocity = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    Rigidbody2D rb;
    public float acceleration = 1f;
    public float jumpheight = 2f;
    public float gravity = 0.5f;

    int groundPoints = 0;
    bool grounded {get => groundPoints > 0;}

    int framesSinceGrounded = 0;

    // Start is called before the first frame update
    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredvelocity = (Vector2.right * Input.GetAxis("Horizontal"));
        
        
    }

    // FixedUpdate is called once per physics frame
    public void FixedUpdate()
    {
        UpdateState();   

        Vector2 change = desiredvelocity - velocity;
        if(change.magnitude > acceleration)
        {
            change = change.normalized * acceleration;
        }
        if (!grounded)
            change += Vector2.down * gravity ;
        else if (Input.GetButton("Jump"))
        {
            change += Vector2.up * jumpheight * gravity * gravity;
        }
        rb.velocity += change;
    }

    public void LateUpdate()
    {
        groundPoints = 0;
    }

    /**
     * UpdateState Updates the physics state of the player Object
     */
    void UpdateState()
    {
        framesSinceGrounded++;
        velocity = rb.velocity;
        if (grounded)
        {
            framesSinceGrounded = 0;
        }
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
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                groundPoints++;
            }
        }
    }
}
