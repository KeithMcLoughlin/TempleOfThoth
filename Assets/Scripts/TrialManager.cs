using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public class TrialManager : MonoBehaviour
{
    public static TrialManager Instance { get; private set; }

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
        var beginningRoomScript = GetComponent<BeginningRoom>();
        var room = beginningRoomScript.Intialise(new Vector3(200, 0, 0), effectiveTraits, ineffectiveTraits);

        //set trial document generator
        PlayerData.Instance.DocumentGeneratorForCurrentTrial = new BeginingRoomDocumentGenerator();

        PlayerController.Instance.transform.position = room.transform.Find("StartPoint").position;
    }
	
	void Update ()
    {
		
	}
}
