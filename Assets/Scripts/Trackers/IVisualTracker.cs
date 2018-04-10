using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public abstract class IVisualTracker : MonoBehaviour
{
    public List<VisualTrackerObjectDetails> trackableObjectsLookedAt = new List<VisualTrackerObjectDetails>();
    public List<VisualTrackerObjectDetails> TrackableObjectsLookedAt
    {
        get
        {
            return trackableObjectsLookedAt;
        }
    }

    public abstract void ResetData();
}

public class VisualTrackerObjectDetails
{
    public TrackableEventObject ObjectLookedAt { get; set; }
    public float TimeSpentLookingAtObject
    {
        //return time spent rounded to two decimal places
        get
        {
            return Mathf.Round(timeSpentLookingAtObject * 100f) / 100f;
        }
        set
        {
            timeSpentLookingAtObject = value;
        }
    }
    private float timeSpentLookingAtObject;

    public VisualTrackerObjectDetails(TrackableEventObject objectLookedAt)
    {
        ObjectLookedAt = objectLookedAt;
        timeSpentLookingAtObject = 0.0f;
    }
}
