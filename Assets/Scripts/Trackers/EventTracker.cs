using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public class EventTracker : IEventTracker
{
    public override void TrackEvent(TrackableEventObject eventObject)
    {
        //Debug.Log("Event Tracked is colour: " + eventObject.Traits.Colour);
        //Debug.Log("Event tracked direction: " + eventObject.Traits.Direction);
        //Debug.Log("Event tracked lighting: " + eventObject.Traits.Lighting);
        ObjectsTracked.Add(eventObject);
    }

    public override void ResetData()
    {
        ObjectsTracked = new List<TrackableEventObject>();
    }
}
