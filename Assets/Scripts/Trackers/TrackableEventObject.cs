using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Trackers
{
    public class TrackableEventObject : MonoBehaviour
    {
        public ObjectTraits Traits { get; set; }

        public void TrackEvent()
        {
            PlayerTracker.Instance.EventTracker.TrackEvent(this);
        }
    }
}
