using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start ()
    {
        speed = 5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        Move(horizontalInput, verticalInput);
    }

    void Move(float horizontal, float vertical)
    {
        var zMovement = vertical * speed * Time.deltaTime;
        var xMovement = horizontal * speed * Time.deltaTime;

        transform.Translate(xMovement, 0f, zMovement);
    }
}
