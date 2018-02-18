using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Trackers;

public interface IEventTracker
{
    void TrackEvent(TrackableEventObject eventObject);
}
