using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public class EventTracker : IEventTracker
{
    public void TrackEvent(TrackableEventObject eventObject)
    {
        Debug.Log("Event Tracked is colour: " + eventObject.Traits.Colour);
        Debug.Log("Event tracked direction: " + eventObject.Traits.Direction);
        Debug.Log("Event tracked lighting: " + eventObject.Traits.Lighting);
        PlayerTracker.Instance.UpdatePlayerData(eventObject.Traits);
    }
}
