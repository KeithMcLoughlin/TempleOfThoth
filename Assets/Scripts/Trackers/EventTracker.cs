using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTracker : IEventTracker
{
    public void TrackEvent(ITrackableEvent eventObject)
    {
        Debug.Log("Event Tracked is colour: " + eventObject.Traits.Colour);
        PlayerTracker.Instance.UpdatePlayerData(eventObject.Traits);
    }
}
