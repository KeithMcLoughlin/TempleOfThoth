using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicAgent : MonoBehaviour {

    static LogicAgent instance;
    public static LogicAgent Instance
    {
        get
        {
            if (instance == null)
            {
                var LogicAgentGameObject = new GameObject();
                instance = LogicAgentGameObject.AddComponent<LogicAgent>();
            }
            return instance;
        }
    }

    PlayerTracker tracker;

    void Start()
    {
        tracker = PlayerTracker.Instance;
    }

	public void IntialiseInteractable(GameObject visualObject)
    {
        var interactablesRenderer = visualObject.GetComponent<Renderer>();
        if (tracker.playerMostInteractedColour != Color.clear)
        {
            interactablesRenderer.material.SetColor("_Color", tracker.playerMostInteractedColour);
        }
        else
        {
            interactablesRenderer.material.SetColor("_Color", Random.ColorHSV());
        }
    }
}
