using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Assets.Scripts.Trackers;

public class Button : TrackableEventObject, IInteractable
{
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
}
