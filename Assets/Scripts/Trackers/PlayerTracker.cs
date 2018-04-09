using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Trackers;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }
    public IEventTracker EventTracker {
        get
        {
            return eventTracker;
        }
    }
    public IVisualTracker VisualTracker
    {
        get
        {
            return visualTracker;
        }
    }

    IEventTracker eventTracker;
    IVisualTracker visualTracker;

    void Awake()
    {
        Instance = this;
        eventTracker = GetComponent<IEventTracker>();
        visualTracker = GetComponent<IVisualTracker>();
    }

    private void Start()
    {
        TrialManager.Instance.OnCurrentTrialCompleted += UpdatePlayerData;
    }

    public void UpdatePlayerData()
    {
        //get data from both trackers + pass to player data
        Debug.Log("Updating player data");
        PlayerData.Instance.InsertTrialData(eventTracker.ObjectsTracked, visualTracker.TrackableObjectsLookedAt);

        //reset the data for the next trial
        eventTracker.ResetData();
        visualTracker.ResetData();
    }
}
