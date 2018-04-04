using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public class TrialManager : MonoBehaviour
{
    public static TrialManager Instance { get; private set; }
    BeginningRoom beginningRoom;

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
        ObjectTraits effectiveTraits;
        ObjectTraits ineffectiveTraits;
        Debug.Log("Querying Logic Agent");
        LogicAgent.Instance.CalculatePlayerPreferrences(out effectiveTraits, out ineffectiveTraits);
        //IntialiseRoom(beginningRoomScript, effectiveTraits, ineffectiveTraits);
        beginningRoom = GetComponent<BeginningRoom>();
        var room = beginningRoom.Intialise(new Vector3(0, 0, 0), effectiveTraits, ineffectiveTraits);

        //set trial document generator
        PlayerData.Instance.DocumentGeneratorForCurrentTrial = new BeginingRoomDocumentGenerator();

        PlayerController.Instance.transform.position = room.transform.Find("StartPoint").position;
    }
	
	void Update ()
    {
		if(beginningRoom.completed)
        {
            Instantiate(Resources.Load("50_50 Decision"), beginningRoom.nextTrialPosition);
            beginningRoom.completed = false;
        }
	}
}
