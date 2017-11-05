using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventTracker
{
    void TrackEvent(ITrackableEvent eventObject);
}
