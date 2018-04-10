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
    public float TimeSpentLookingAtObject { get; set; }

    public VisualTrackerObjectDetails(TrackableEventObject objectLookedAt)
    {
        ObjectLookedAt = objectLookedAt;
        TimeSpentLookingAtObject = 0.0f;
    }
}
