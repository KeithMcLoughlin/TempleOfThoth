using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public abstract class IEventTracker : MonoBehaviour
{
    public List<TrackableEventObject> ObjectsTracked = new List<TrackableEventObject>();
    public abstract void TrackEvent(TrackableEventObject eventObject);

    public abstract void ResetData();
}
