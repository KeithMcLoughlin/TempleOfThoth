using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Trackers;

namespace Assets.Scripts.Interactables
{
    public class Pedastal : TrackableEventObject, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Pedastal clicked");
        }
    }
}
