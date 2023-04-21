using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyCharacterController : MonoBehaviour
{
    [SerializeField] float gravity = 1;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float jumpSpeed = 1;
    [SerializeField] float speed = 1;
    CharacterController controller;

    public UnityEngine.Events.UnityEvent PlayerHit;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        if (!controller.isGrounded)
            jumpSpeed = jumpSpeed - (gravity * Time.deltaTime);
        else
            {
                if (Input.GetButton("Jump"))
                    jumpSpeed = jumpForce;
                else jumpSpeed = 0;
            };
        input.y = jumpSpeed;
        controller.Move(input * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        ThunderCloud Cloud = collision.gameObject.GetComponent<ThunderCloud>();
        if (Cloud && Cloud.state != 0)
        {
            PlayerHit.Invoke();
            Debug.Log("PlayerHit");
        }
        Debug.Log("BONK!");
    }
}
