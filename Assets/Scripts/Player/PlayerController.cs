using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    public float speed = 5f; //speed value for player movement and jumping
    public float runSpeedIncrease = 5f; //the multiple increase in speed when running
    public float maxJumpDuration = 1.0f; //how long the player will rise when jumping
    bool jumping = false;
    float currentJumpDuration;
    Rigidbody playerRigidBody;
    PlayerController playerMovementScript;
    CameraMouseMovement cameraMovementScript;

    void Awake()
    {
        Instance = this;
        currentJumpDuration = 0.0f;
    }

    void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerMovementScript = GetComponent<PlayerController>();
        cameraMovementScript = GetComponentInChildren<CameraMouseMovement>();
	}

    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        var jumpInput = Input.GetAxisRaw("Jump");
        //player is running only if the run button is pressed and they are moving forward
        //they cannot run backwards or sideways
        var running = Input.GetAxisRaw("Run") > 0 && verticalInput > 0;
        
        Move(horizontalInput, verticalInput, running);
        Jump(jumpInput);
    }

    void Jump(float jumpInput)
    {
        //if pressing the jump button and not currently jumping
        if (!jumping && jumpInput > 0)
            jumping = true;

        if (jumping)
        {
            //increase players Y while jumping
            var yMovement = speed * Time.deltaTime;
            transform.Translate(0, yMovement, 0);

            currentJumpDuration += maxJumpDuration * Time.deltaTime;
            //stop jumping if the duration has been hit
            if (currentJumpDuration > maxJumpDuration)
            {
                currentJumpDuration = 0.0f;
                jumping = false;
            }
        }
    }

    void Move(float horizontal, float vertical, bool running)
    {
        var zMovement = vertical * speed * Time.deltaTime;
        var xMovement = horizontal * speed * Time.deltaTime;

        //increase forward speed if running
        if (running) zMovement *= runSpeedIncrease;

        transform.Translate(xMovement, 0, zMovement);
    }

    public void Dead()
    {
        playerMovementScript.enabled = false;
        cameraMovementScript.enabled = false;

        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationY;
        transform.Rotate(Vector3.forward);
    }
}
