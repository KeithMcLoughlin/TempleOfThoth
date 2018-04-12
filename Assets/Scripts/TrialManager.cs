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
        PlayerData.Instance.DocumentGeneratorForCurrentTrial = beginningRoom.DocumentGeneratorForTrial;
    }

    void LoadNextTrial(Transform nextTrialPosition)
    {
        //notify that the current trial is completed
        TrialCompleted();

        var moreTrials = TrialsInOrder.Count > 0;
        if (moreTrials)
        {
            //get next trial script from the stack
            var nextTrial = TrialsInOrder.Pop();
            //query for instructions for setting trial up
            LogicAgent.Instance.GetTrialBeginningInstructions(nextTrial, out predicatedBestTraits, out predicatedWorstTraits);
            //intialise trial + provide it the instructions recieved from the agent
            nextTrial.Intialise(nextTrialPosition);
            nextTrial.ProvideSetupInstructions(predicatedBestTraits, predicatedWorstTraits);
            //setup its completed event to this method
            nextTrial.OnTrialCompleted += LoadNextTrial;
            //provide the new trial document generator for the data manager
            PlayerData.Instance.DocumentGeneratorForCurrentTrial = nextTrial.DocumentGeneratorForTrial;
        }
        else
        {
            //todo allow for game to fully be completed
            Debug.Log("Game Completed");
        }
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
