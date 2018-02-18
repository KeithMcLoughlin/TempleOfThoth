using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public class EventTracker : IEventTracker
{
    public void TrackEvent(TrackableEventObject eventObject)
    {
        Debug.Log("Event Tracked is colour: " + eventObject.Traits.Colour);
        PlayerTracker.Instance.UpdatePlayerData(eventObject.Traits);
    }
}
