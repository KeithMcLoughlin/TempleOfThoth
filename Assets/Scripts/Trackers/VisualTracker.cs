using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Trackers
{
    public class VisualTracker : MonoBehaviour, IVisualTracker
    {
        public float maxTrackableObjectDistance = 2.5f;

        Ray visualTrackerRay;
        RaycastHit trackableObjectPlayerIsLookingAt;
        int trackableObjectLayer;
        Camera playersVision;

        void Start()
        {
            playersVision = GetComponent<Camera>();
            trackableObjectLayer = LayerMask.GetMask("TrackableObject");
        }

        void Update()
        {
            //setup the ray for determining what the player is looking at
            visualTrackerRay.origin = this.transform.position;
            visualTrackerRay.direction = this.transform.forward;

            //if player is looking at a trackable object
            if (Physics.Raycast(visualTrackerRay, out trackableObjectPlayerIsLookingAt, maxTrackableObjectDistance, trackableObjectLayer))
            {
                var trackableObjectScript = trackableObjectPlayerIsLookingAt.collider.GetComponent<TrackableEventObject>();
                var traits = trackableObjectScript.Traits;
                Debug.Log("Looking at \"" + trackableObjectScript.name + "\" of colour " + traits.Colour);
            }
        }
    }
}
