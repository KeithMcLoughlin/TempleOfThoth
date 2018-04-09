using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public abstract class IVisualTracker : MonoBehaviour
{
    public Dictionary<TrackableEventObject, float> trackableObjectsLookedAt = new Dictionary<TrackableEventObject, float>();
    public Dictionary<TrackableEventObject, float> TrackableObjectsLookedAt
    {
        get
        {
            return trackableObjectsLookedAt;
        }
    }

    public abstract void ResetData();
}
