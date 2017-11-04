using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }

    public InteractableTracker InteractableTracker;
    //public VisualTracker VisualTracker;
    //public MovementTracker MovementTracker;

    void Start()
    {
        Instance = this;
    }

    void UpdatePlayerData()
    {

    }
}
