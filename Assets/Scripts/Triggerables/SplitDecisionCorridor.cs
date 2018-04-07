using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Trackers;
using UnityEngine;

namespace Assets.Scripts.Triggerables
{
    class SplitDecisionCorridor : TrackableEventObject
    {
        public delegate void CorridorChosen();
        public event CorridorChosen OnCorridorChosen;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player Made Decision");
            TrackEvent();

            //notify that player chose this corridor
            if (OnCorridorChosen != null)
            {
                OnCorridorChosen();
            }
        }

        public void SetTraits(ObjectTraits traits)
        {
            Traits = traits;
        }
    }
}
