using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    public InteractableTraits Traits { get; set; }

    IInteractableTracker tracker;
    Renderer buttonRenderer;

    // Use this for initialization
    void Start () {
        //tracker = PlayerTracker.Instance.InteractableTracker;
        //buttonRenderer = GetComponent<Renderer>();
        //buttonRenderer.material.SetColor("_Color", Color.green);
    }

    public void Interact()
    {
        //buttonRenderer.material.SetColor("_Color", Color.green);
        //AnalyticsScript.TriggerEvent();
        tracker.TrackInteraction(this);
    }
}
