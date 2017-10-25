using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {

    static PlayerTracker instance;
    public static PlayerTracker Instance
    {
        get
        {
            if (instance == null)
            {
                var PlayerTrackerGameObject = new GameObject();
                instance = PlayerTrackerGameObject.AddComponent<PlayerTracker>();
            }
            return instance;
        }
    }

    //the clear colour will be used as a null if there is no clear preferred colour
    public Color playerMostInteractedColour = Color.clear;
    GameObject analyticsTestCube;
    Dictionary<Color, int> colourInteractedAmounts = new Dictionary<Color, int>();
    LogicAgent agent;

    void Start()
    {
        agent = LogicAgent.Instance;
        analyticsTestCube = GameObject.Find("AnalyticTestCube");
    }

	public void ProcessInteractable(InteractableType typeOfInteractable, Color interactableColour)
    {
        int numOfTimesInteracted;
        //get the number of times colour was interacted with if it already exists in the dictionary
        if(colourInteractedAmounts.TryGetValue(interactableColour, out numOfTimesInteracted))
        {
            colourInteractedAmounts[interactableColour] = ++numOfTimesInteracted;
        }
        else
        {
            //add new colour with one interaction to the dictionary as it is new
            colourInteractedAmounts.Add(interactableColour, 1);
        }

        playerMostInteractedColour = colourInteractedAmounts.FirstOrDefault(x => x.Value == colourInteractedAmounts.Values.Max()).Key;


        //NOT NEEDED: temp code for testing agent intialisation
        agent.IntialiseInteractable(analyticsTestCube);
    }
}
