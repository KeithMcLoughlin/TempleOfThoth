using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ITrackableEvent {

    public float speed = 2f;
    public float openDistance = 10f;

    public ObjectTraits Traits { get; private set; }
    public IEventTracker Tracker { get; private set; }

    Vector3 openPosition;
    Vector3 closePosition;
    PlayerController player;
    bool playerInRange;
    
	void Start ()
    {
        Tracker = PlayerTracker.Instance.EventTracker;
        Traits = new ObjectTraits(Color.green, Direction.Left, Size.Medium);
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Traits.Colour);
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

    private void OnTriggerEnter(Collider other)
    {
        TrackEvent();
    }

    public void TrackEvent()
    {
        Tracker.TrackEvent(this);
    }
}
