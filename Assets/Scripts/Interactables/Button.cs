using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Button : MonoBehaviour, IInteractable, ITrackableEvent
{
    public ObjectTraits Traits { get; set; }
    public IEventTracker Tracker { get; private set; }
    Renderer buttonRenderer;
    
    void Start () {
        Tracker = PlayerTracker.Instance.EventTracker;
        Traits = new ObjectTraits(Color.red, Position.Right, Size.Medium);
        buttonRenderer = GetComponent<Renderer>();
        buttonRenderer.material.SetColor("_Color", Traits.Color);
    }

    public void Interact()
    {
        TrackEvent();
    }

    public void TrackEvent()
    {
        Tracker.TrackEvent(this);
    }
}
