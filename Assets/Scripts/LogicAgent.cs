using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicAgent : MonoBehaviour
{
    public static LogicAgent Instance { get; set; }
    List<GameObject> alterableObjects;


    void Start()
    {
        Instance = this;
    }

	public void IntialiseInteractable(GameObject visualObject)
    {
        /*var interactablesRenderer = visualObject.GetComponent<Renderer>();
        if (tracker.playerMostInteractedColour != Color.clear)
        {
            interactablesRenderer.material.SetColor("_Color", tracker.playerMostInteractedColour);
        }
        else
        {
            interactablesRenderer.material.SetColor("_Color", Random.ColorHSV());
        }*/
    }
}
