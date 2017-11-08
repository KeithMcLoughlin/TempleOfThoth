using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable, ITrackableEvent {

    public ObjectTraits Traits { get; set; }
    Renderer pickupRenderer;
    
    void Start()
    {
    }

    public void Interact()
    {
        TrackEvent();
    }

    public void TrackEvent()
    {
        PlayerTracker.Instance.EventTracker.TrackEvent(this);
    }
}
