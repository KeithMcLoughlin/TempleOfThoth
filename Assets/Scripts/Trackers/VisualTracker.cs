using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Data;

namespace Assets.Scripts.Trackers
{
    public class VisualTracker : IVisualTracker
    {
        public float maxTrackableObjectDistance = 2.5f;

        Ray visualTrackerRay = new Ray();
        RaycastHit trackableObjectPlayerIsLookingAt;
        int trackableObjectLayer;
        Camera playersVision;

        void Start()
        {
            playersVision = PlayerController.Instance.transform.Find("Main Camera").GetComponent<Camera>();
            trackableObjectLayer = LayerMask.NameToLayer("TrackableObject");
        }

        void Update()
        {
            //setup the ray for determining what the player is looking at
            visualTrackerRay.origin = playersVision.transform.position;
            visualTrackerRay.direction = playersVision.transform.forward;

            //if player is looking at a trackable object
            if (Physics.Raycast(visualTrackerRay, out trackableObjectPlayerIsLookingAt, maxTrackableObjectDistance))
            {
                //Debug.Log(trackableObjectPlayerIsLookingAt.collider.name);
                var trackableObject = trackableObjectPlayerIsLookingAt.collider.GetComponent<TrackableEventObject>();
                var sectionOfTrackableObject = trackableObjectPlayerIsLookingAt.collider.GetComponent<SectionOfTrackableEventObject>();
                if (trackableObject != null && trackableObject.Traits != null)
                {
                    EditObjectsSeen(trackableObject);
                }
                else if(sectionOfTrackableObject != null && sectionOfTrackableObject.ParentTrackableEventObject.Traits != null)
                {
                    EditObjectsSeen(sectionOfTrackableObject.ParentTrackableEventObject);
                }
            }
        }

        public override void ResetData()
        {
            trackableObjectsLookedAt.Clear();
        }

        private void EditObjectsSeen(TrackableEventObject trackableObject)
        {
            var traits = trackableObject.Traits;
            Debug.Log("Looking at \"" + trackableObject.name + "\" of colour " + traits.Colour);

            //check if the object has already been seen and add to the time spent looking at it
            var objectAlreadySeen = trackableObjectsLookedAt.Find(x => x.ObjectLookedAt == trackableObject);
            if (objectAlreadySeen != null)
            {
                //Debug.Log("Object already seen for " + trackableObjectsLookedAt[trackableObject] + " seconds");
                objectAlreadySeen.TimeSpentLookingAtObject += Time.deltaTime;
            }
            //else add the object to the list of objects looked at by the player
            else
            {
                //Debug.Log("Newly seen object");
                trackableObjectsLookedAt.Add(new VisualTrackerObjectDetails(trackableObject));
            }
        }
    }
}
