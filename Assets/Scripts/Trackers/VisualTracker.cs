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
        public Dictionary<TrackableEventObject, float> trackableObjectsLookedAt = new Dictionary<TrackableEventObject, float>();

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
                var trackableObject = trackableObjectPlayerIsLookingAt.collider.GetComponent<TrackableEventObject>();
                var traits = trackableObject.Traits;
                Debug.Log("Looking at \"" + trackableObject.name + "\" of colour " + traits.Colour);
                
                if(trackableObjectsLookedAt.ContainsKey(trackableObject))
                {
                    Debug.Log("Object already seen for " + trackableObjectsLookedAt[trackableObject] + " seconds");
                    trackableObjectsLookedAt[trackableObject] += Time.deltaTime;
                    if(trackableObjectsLookedAt[trackableObject] > 10)
                    {
                        SaveData();
                        ResetData();
                    }
                }
                else
                {
                    Debug.Log("Newly seen object");
                    trackableObjectsLookedAt.Add(trackableObject, 0.0f);
                }
            }
        }

        void ResetData()
        {
            trackableObjectsLookedAt.Clear();
        }

        void SaveData()
        {
            //temp solution for inserting data
            PlayerData.Instance.InsertUserTrialVisualData("Beginning Room", trackableObjectsLookedAt);
        }
    }
}
