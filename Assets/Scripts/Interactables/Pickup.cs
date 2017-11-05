using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable, ITrackableEvent {

    public ObjectTraits Traits { get; set; }
    public IEventTracker Tracker { get; private set; }
    Renderer pickupRenderer;

    // Use this for initialization
    void Start()
    {
        Tracker = PlayerTracker.Instance.EventTracker;
        //buttonRenderer = GetComponent<Renderer>();
        //buttonRenderer.material.SetColor("_Color", Color.green);
    }

    public void Interact()
    {
        //buttonRenderer.material.SetColor("_Color", Color.green);
        //AnalyticsScript.TriggerEvent();
        TrackEvent();
    }

    public void TrackEvent()
    {
        Tracker.TrackEvent(this);
    }
}
