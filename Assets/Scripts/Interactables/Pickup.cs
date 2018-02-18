using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public class Pickup : TrackableEventObject, IInteractable {
    
    Renderer pickupRenderer;
    
    void Start()
    {
    }

    public void Interact()
    {
        TrackEvent();
    }
}
