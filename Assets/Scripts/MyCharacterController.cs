using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MyCharacterController : MonoBehaviour
{
    [SerializeField] float gravity = 1;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float jumpSpeed = 1;
    [SerializeField] float speed = 1;
    CharacterController controller;
    float camMaxX, camMinX;
    public UnityEvent PlayerHit;
    public UnityEvent<int> ChangeFace;
    bool lastGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        camMaxX = Camera.main.transform.position.x + cameraWidth;
        camMinX = Camera.main.transform.position.x - cameraWidth;
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
                if(!lastGrounded)
                {
                    ChangeFace.Invoke(0);
                }
                if (Input.GetButton("Jump"))
                {
                    jumpSpeed = jumpForce;
                    ChangeFace.Invoke(1);
                }
                else jumpSpeed = 0;
            };
        input.y = jumpSpeed;

        Vector2 ourMove = input * Time.deltaTime;
    
        if (transform.position.x + ourMove.x < camMinX)
            ourMove.x = 0;
        if (transform.position.x + ourMove.x > camMaxX)
            ourMove.x = 0;
        controller.Move(ourMove);
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        ThunderCloud Cloud = collision.gameObject.GetComponent<ThunderCloud>();
        if (Cloud && Cloud.state == 2)
        {
            PlayerHurt();
        }
    }

    public void ReadyPlayer()
    {
        ChangeFace.Invoke(0);
    }

    public void PlayerHurt()
    {
        ChangeFace.Invoke(2);
        PlayerHit.Invoke();
    }
}
