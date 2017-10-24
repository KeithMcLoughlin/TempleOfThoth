using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour, Interactable {

    Renderer buttonRenderer;

	// Use this for initialization
	void Start () {
        buttonRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interact()
    {
        Debug.Log("Interacted");
        buttonRenderer.material.SetColor("_Color", Color.green);
    }
}
