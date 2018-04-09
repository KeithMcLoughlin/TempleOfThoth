using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public class TrialManager : MonoBehaviour
{
    public static TrialManager Instance { get; private set; }
    BeginningRoom beginningRoom;
    SplitDecisionTrialRoom splitDecisionTrialRoom;
    public delegate void CurrentTrialCompleted();
    public event CurrentTrialCompleted OnCurrentTrialCompleted;
    Stack<ITrialRoom> TrialsInOrder; 

    void Awake()
    {
        Instance = this;
    }

    ObjectTraits predicatedBestTraits;
    ObjectTraits predicatedWorstTraits;
    GameObject firstRoomObject;
    GameObject player;

    void Start ()
    {
        //Initialise Trial room scripts
        beginningRoom = GetComponent<BeginningRoom>();
        splitDecisionTrialRoom = GetComponent<SplitDecisionTrialRoom>();

        //setup the trials into a stack in the order they will occur
        TrialsInOrder = new Stack<ITrialRoom>();
        TrialsInOrder.Push(splitDecisionTrialRoom);

        //intialise beginning room at the start
        beginningRoom.Intialise(this.transform);
        beginningRoom.OnTrialCompleted += LoadNextTrial;

        //set trial document generator
        PlayerData.Instance.DocumentGeneratorForCurrentTrial = new BeginingRoomDocumentGenerator();
    }

    void LoadNextTrial(Transform nextTrialPosition)
    {
        var nextTrial = TrialsInOrder.Pop();
        LogicAgent.Instance.CalculatePlayerPreferrences(out predicatedBestTraits, out predicatedWorstTraits);
        nextTrial.Intialise(nextTrialPosition);
        nextTrial.ProvideSetupInstructions(predicatedBestTraits, predicatedWorstTraits);
        nextTrial.OnTrialCompleted += LoadNextTrial;
        TrialCompleted();
    }

    private void TrialCompleted()
    {
        Debug.Log("Current Trial Completed");
        //notify subscribers that the current trial completed
        if (OnCurrentTrialCompleted != null)
        {
            OnCurrentTrialCompleted();
        }
    }
}
