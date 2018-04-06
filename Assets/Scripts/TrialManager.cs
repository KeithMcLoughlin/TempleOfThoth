using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public class TrialManager : MonoBehaviour
{
    public static TrialManager Instance { get; private set; }
    BeginningRoom beginningRoom;
    SplitDecisionTrialRoom splitDecisionTrialRoom;

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
        Debug.Log("Querying Logic Agent");
        LogicAgent.Instance.CalculatePlayerPreferrences(out predicatedBestTraits, out predicatedWorstTraits);
        
        //Initialise Trial room scripts
        beginningRoom = GetComponent<BeginningRoom>();
        splitDecisionTrialRoom = GetComponent<SplitDecisionTrialRoom>();
        
        //setup beginning room
        var room = IntialiseRoom(beginningRoom, this.transform, predicatedBestTraits, predicatedWorstTraits);

        //set trial document generator
        PlayerData.Instance.DocumentGeneratorForCurrentTrial = new BeginingRoomDocumentGenerator();

        PlayerController.Instance.transform.position = room.transform.Find("StartPoint").position;
    }
	
	void Update ()
    {
		if(beginningRoom.completed)
        {
            LogicAgent.Instance.CalculatePlayerPreferrences(out predicatedBestTraits, out predicatedWorstTraits);
            IntialiseRoom(splitDecisionTrialRoom, beginningRoom.nextTrialPosition, predicatedBestTraits, predicatedWorstTraits);
            beginningRoom.completed = false;
        }
	}

    GameObject IntialiseRoom(ITrialRoom room, Transform position, ObjectTraits effectiveTriats, ObjectTraits ineffectiveTriats)
    {
        return room.Intialise(position, predicatedBestTraits, predicatedWorstTraits);
    }
}
