using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public class Door : TrackableEventObject {

    public float speed = 2f; //speed the door opens and closes
    public float openDistance = 10f; //the distance the player needs to be to the door before it begins to open
    public bool locked = false;

    //stores the positions that represent the door when it is open or closed
    Vector3 openPosition; 
    Vector3 closePosition;

    PlayerController player;
    bool playerInRange;

    public delegate void DoorTriggered();
    public event DoorTriggered OnDoorTriggered;
    
	void Start ()
    {
        var renderer = GetComponent<Renderer>();
        var doorColour = renderer.material.GetColor("_Color");
        Traits = new ObjectTraits(doorColour, Direction.Left, Size.Medium);

        //close position is where the door start and the open position is the doors height up on the y axis
        openPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        closePosition = transform.position;
        player = PlayerController.Instance;
        playerInRange = false;
    }
	
	void Update ()
    {
        //check if the player is close enough to the door and if the door needs to open
        playerInRange = Vector3.Distance(player.transform.position, transform.position) < openDistance;

        if(locked && transform.position.y <= closePosition.y)
        {
            //show locked symbol
        }
        else
        {
            //open door
            if (transform.position.y <= openPosition.y && playerInRange)
                transform.Translate(0f, speed * Time.deltaTime, 0f);
            //close door
            else if (transform.position.y >= closePosition.y && !playerInRange)
                transform.Translate(0f, -(speed * Time.deltaTime), 0f);

            //adjust position to close or open position if it has gone past it
            if (transform.position.y < closePosition.y)
                transform.position = closePosition;

            if (transform.position.y > openPosition.y)
                transform.position = openPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TrackEvent();

        //notify that player has went through the door
        if(OnDoorTriggered != null)
        {
            OnDoorTriggered();
        }
    }
}
