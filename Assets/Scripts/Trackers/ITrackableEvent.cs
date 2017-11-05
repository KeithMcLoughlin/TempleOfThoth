using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackableEvent
{
    ObjectTraits Traits { get; }
    void TrackEvent();
}
