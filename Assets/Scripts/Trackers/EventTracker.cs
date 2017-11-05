using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTracker : IEventTracker
{
    public void TrackEvent(ITrackableEvent eventObject)
    {
        Debug.Log("Event Tracked is colour: " + eventObject.Traits.Color);
        /*
        int numOfTimesInteracted;
        //get the number of times colour was interacted with if it already exists in the dictionary
        if (colourInteractedAmounts.TryGetValue(interactableColour, out numOfTimesInteracted))
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
        //agent.IntialiseInteractable(analyticsTestCube);*/
    }
}
