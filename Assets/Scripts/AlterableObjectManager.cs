using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterableObjectManager : MonoBehaviour
{
    public static AlterableObjectManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public static List<Color> UsableColours = 
        new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };
    public static Color GetRandomColour()
    {
        return UsableColours[Random.Range(0, UsableColours.Count - 1)];
    }
    public static Color GetRandomColour(IList<Color> selectableColours)
    {
        return selectableColours[Random.Range(0, selectableColours.Count - 1)];
    }

    ObjectTraits predicatedBestTraits;
    ObjectTraits predicatedWorstTraits;
    GameObject firstRoomObject;
    GameObject player;

    void Start ()
    {
        ObjectTraits effectiveTraits;
        ObjectTraits ineffectiveTraits;
        Debug.Log(LogicAgent.Instance);
        LogicAgent.Instance.CalculatePlayerPreferrences(out effectiveTraits, out ineffectiveTraits);
        //IntialiseRoom(beginningRoomScript, effectiveTraits, ineffectiveTraits);
        var beginningRoomScript = GetComponent<BeginningRoom>();
        var room = beginningRoomScript.Intialise(new Vector3(200, 0, 0), effectiveTraits, ineffectiveTraits);

        PlayerController.Instance.transform.position = room.transform.Find("StartPoint").position;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void IntialiseRoom(ITrialRoom roomScript, ObjectTraits bestTraits, ObjectTraits worstTraits)
    {
        roomScript.IntialiseTraits(bestTraits, worstTraits);
    }
}
