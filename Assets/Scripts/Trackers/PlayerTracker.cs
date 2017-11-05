using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }

    public EventTracker EventTracker;
    //public VisualTracker VisualTracker;
    //public MovementTracker MovementTracker;

    void Awake()
    {
        Instance = this;
        EventTracker = new EventTracker();
    }

    void UpdatePlayerData()
    {

    }
}
