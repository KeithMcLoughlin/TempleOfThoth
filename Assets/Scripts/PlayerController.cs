using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float runSpeedIncrease = 5f;
    bool jumping = false;
    float peakOfJump;
    Rigidbody playerRigidBody;
    PlayerController playerMovementScript;
    CameraMouseMovement cameraMovementScript;

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerMovementScript = GetComponent<PlayerController>();
        cameraMovementScript = GetComponentInChildren<CameraMouseMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        //todo fix jump
        var jumpInput = Input.GetAxisRaw("Jump");
        var running = Input.GetAxisRaw("Run") > 0 && verticalInput > 0;

        if(!jumping && jumpInput > 0)
        {
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumping = true;
        }

        Move(horizontalInput, verticalInput, running);
    }

    void Move(float horizontal, float vertical, bool running)
    {
        var zMovement = vertical * speed * Time.deltaTime;
        var xMovement = horizontal * speed * Time.deltaTime;
        var yMovement = jumping ? speed * Time.deltaTime : 0f;

        if (running) zMovement *= runSpeedIncrease;

        transform.Translate(xMovement, yMovement, zMovement);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Walkable")
           jumping = false;
    }

    void Dead()
    {
        playerMovementScript.enabled = false;
        cameraMovementScript.enabled = false;
    }
}
