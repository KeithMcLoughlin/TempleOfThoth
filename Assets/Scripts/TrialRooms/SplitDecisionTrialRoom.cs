using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SplitDecisionTrialRoom : MonoBehaviour, ITrialRoom {

    GameObject Room;

    public GameObject Intialise(Transform position, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        Room = Instantiate(Resources.Load("50_50 Decision"), position) as GameObject;

        return Room;
    }
}
