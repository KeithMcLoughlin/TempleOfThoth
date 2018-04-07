using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class SplitDecisionTrialRoom : MonoBehaviour, ITrialRoom {

    public int totalNumbeerOfDecisions = 2;
    GameObject Room;

    public GameObject Intialise(Transform position, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        Room = Instantiate(Resources.Load("50_50 Decision"), position) as GameObject;

        Transform deadendCorridor;
        Transform progressCorridor;
        if (effectiveTraits.Direction == Direction.Left)
        {
            deadendCorridor = Room.transform.Find("Right Angle Corridor (Left Side)");
            progressCorridor = Room.transform.Find("Right Angle Corridor (Right Side)");
        }
        else
        { 
            deadendCorridor = Room.transform.Find("Right Angle Corridor (Right Side)");
            progressCorridor = Room.transform.Find("Right Angle Corridor (Left Side)");
        }
        
        SetupDeadEndCorridor(deadendCorridor);
        SetupProgressCorridor(progressCorridor);

        ColourCorridor(deadendCorridor, effectiveTraits.Colour);
        ColourCorridor(progressCorridor, ineffectiveTraits.Colour);

        return Room;
    }

    void SetupDeadEndCorridor(Transform corridor)
    {
        //enable dead end wall
        var deadendWall = corridor.Find("DeadEndWall");
        deadendWall.gameObject.SetActive(true);
        //spawn statue at ai position
        var statuePosition = corridor.Find("StatuePosition");
        Instantiate(Resources.Load("AIEnemy"), statuePosition);
    }

    void SetupProgressCorridor(Transform corridor)
    {
        //create next 50 50 trial corridor extended from the progression corridor
        var corridorPosition = corridor.Find("Progress Position");
        Instantiate(Resources.Load("50_50 Decision"), corridorPosition);
    }

    void ColourCorridor(Transform corridor, Color colour)
    {
        List<Transform> sections = new List<Transform>();
        sections.Add(corridor.Find("First Part"));
        sections.Add(corridor.Find("Second Part"));
        sections.Add(corridor.Find("Intersection"));

        foreach (var section in sections)
        {
            section.Find("Floor").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("RightWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("LeftWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("Roof").GetComponent<Renderer>().material.SetColor("_Color", colour);
        }

        corridor.Find("DeadEndWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
    }
}
