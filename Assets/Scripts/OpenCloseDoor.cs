using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour {

    public float speed = 2f;
    public float openDistance = 10f;

    Vector3 openPosition;
    Vector3 closePosition;
    PlayerController player;
    bool playerInRange;
    
	void Start ()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.green);
        openPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        closePosition = transform.position;
        player = PlayerController.Instance;
        playerInRange = false;
    }
	
	void Update ()
    {
        playerInRange = Vector3.Distance(player.transform.position, transform.position) < openDistance;

        //open door
        if (transform.position.y <= openPosition.y && playerInRange)
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        //close door
        else if (transform.position.y >= closePosition.y && !playerInRange)
            transform.Translate(0f, -(speed * Time.deltaTime), 0f);
    }
}
