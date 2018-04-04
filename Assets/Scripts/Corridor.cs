using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {

    public Door front;
    public Door back;
    public Transform nextTrialPosition;
    public delegate void CorridorEntered(object sender);
    public event CorridorEntered OnCorridorEntered;


    void Awake ()
    {
        front.OnDoorTriggered += CorridorEnteredThroughFront;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CorridorEnteredThroughFront()
    {
        //notify that player has went entered corridor so they can be used as load zones
        if (OnCorridorEntered != null)
        {
            OnCorridorEntered(this);
        }
    }
}
