using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Button : MonoBehaviour, IInteractable, ITrackableEvent
{
    public ObjectTraits Traits { get; set; }
    Renderer buttonRenderer;
    
    void Start ()
    {
        Traits = new ObjectTraits(Color.red, Direction.Right, Size.Medium);
        buttonRenderer = GetComponent<Renderer>();
        buttonRenderer.material.SetColor("_Color", Traits.Colour);
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
