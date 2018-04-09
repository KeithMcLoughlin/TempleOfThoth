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
        //PlayerData.Instance.UpdateData(traits);
        //get data from both trackers + pass to player data
        Debug.Log("Updating player data");
    }
}
