using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {

    public Door front;
    public Transform nextTrialPosition;
    public delegate void CorridorEntered(object sender);
    public event CorridorEntered OnCorridorEntered;

    private void OnTriggerEnter(Collider other)
    {
        front.TrackEvent();

        //notify that player has entered corridor so they can be used as load zones
        if (OnCorridorEntered != null)
        {
            OnCorridorEntered(this);
        }
    }
}
