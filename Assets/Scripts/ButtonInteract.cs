using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ButtonInteract : MonoBehaviour, Interactable {
    
    PlayerTracker tracker;

    Renderer buttonRenderer;

	// Use this for initialization
	void Start () {
        tracker = PlayerTracker.Instance;
        buttonRenderer = GetComponent<Renderer>();
        //buttonRenderer.material.SetColor("_Color", Color.green);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Interact()
    {
        //buttonRenderer.material.SetColor("_Color", Color.green);
        //AnalyticsScript.TriggerEvent();
        tracker.ProcessInteractable(InteractableType.Button, buttonRenderer.material.color);
    }
}
