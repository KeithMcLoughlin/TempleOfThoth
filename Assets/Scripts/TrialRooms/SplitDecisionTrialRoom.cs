using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Triggerables;

public class SplitDecisionTrialRoom : ITrialRoom {

    public int totalNumberOfDecisions = 3;
    int numberOfChoicesMade = 0;
    GameObject Room;
    GameObject NextDecisionCorridor;
    GameObject CurrentDeadEndCorridor;
    GameObject CurrentProgressionCorridor;
    Direction deadendDirection;
    Direction progressionDirection;

    override public void Intialise(Transform position)
    {
        Room = Instantiate(Resources.Load("50_50 Decision"), position) as GameObject;
    }

    public override void ProvideSetupInstructions(ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        SetupDecision(Room, effectiveTraits, ineffectiveTraits, false);
    }

    void SetupDeadEndCorridor(Transform corridor)
    {
        //enable dead end wall
        var deadendWall = corridor.Find("DeadEndWall");
        deadendWall.gameObject.SetActive(true);
        //spawn statue at ai position
        var statuePosition = corridor.Find("StatuePosition");
        Instantiate(Resources.Load("AIEnemy"), statuePosition);
        CurrentDeadEndCorridor = corridor.gameObject;
    }

    void SetupProgressCorridor(Transform corridor, string prefabName)
    {
        //create next corridor extended from the progression corridor
        var corridorPosition = corridor.Find("Progress Position");
        NextDecisionCorridor = Instantiate(Resources.Load(prefabName), corridorPosition) as GameObject;
        CurrentProgressionCorridor = corridor.gameObject;
    }

    void ColourCorridor(Transform corridor, Color colour)
    {
        List<Transform> sections = new List<Transform>();
        //the three sections of the corridor to be coloured
        sections.Add(corridor.Find("First Part"));
        sections.Add(corridor.Find("Second Part"));
        sections.Add(corridor.Find("Intersection"));

        //loop through each sections parts and colour them
        foreach (var section in sections)
        {
            section.Find("Floor").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("RightWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("LeftWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
            section.Find("Roof").GetComponent<Renderer>().material.SetColor("_Color", colour);
        }

        //colour the deadend wall
        corridor.Find("DeadEndWall").GetComponent<Renderer>().material.SetColor("_Color", colour);
    }

    void SetupLighting(Transform corridor, Lighting lighting)
    {
        float lightIntensity = 0.0f;
        switch(lighting)
        {
            case Lighting.None: { lightIntensity = 0; break; }
            case Lighting.Dim: { lightIntensity = 1; break; }
            case Lighting.Normal: { lightIntensity = 2; break; }
            case Lighting.Bright: { lightIntensity = 3; break; }
        }

        corridor.Find("Spotlight").GetComponent<Light>().intensity = lightIntensity;
    }

    void SetupDecision(GameObject corridor, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits, bool lastDecision)
    {
        Transform deadendCorridor;
        Transform progressCorridor;
        //determine which side will be the deadend and which side will be the progression corridor based 
        //on what is predicted to lure the player to go down the deadend corridor
        if (effectiveTraits.Direction == Direction.Left)
        {
            deadendCorridor = corridor.transform.Find("Right Angle Corridor (Left Side)");
            progressCorridor = corridor.transform.Find("Right Angle Corridor (Right Side)");
            deadendDirection = Direction.Left;
            progressionDirection = Direction.Right;
        }
        else
        {
            deadendCorridor = corridor.transform.Find("Right Angle Corridor (Right Side)");
            progressCorridor = corridor.transform.Find("Right Angle Corridor (Left Side)");
            deadendDirection = Direction.Right;
            progressionDirection = Direction.Left;
        }

        //setup the deadend and progression corridors
        SetupDeadEndCorridor(deadendCorridor);
        //provide the end goal if its the last decision the player needs to make or else continue building the trial
        if(lastDecision)
            SetupProgressCorridor(progressCorridor, "GoalDoorwayCorridor");
        else
            SetupProgressCorridor(progressCorridor, "50_50 Decision");
        
        //colour the corridor based on the predicted traits
        ColourCorridor(deadendCorridor, effectiveTraits.Colour);
        ColourCorridor(progressCorridor, ineffectiveTraits.Colour);

        //light up the corridors based on the predicted traits
        SetupLighting(deadendCorridor, effectiveTraits.Lighting);
        SetupLighting(progressCorridor, ineffectiveTraits.Lighting);

        //subscribe the next decision step to the triggers in the corridor
        var deadendCorridorScript = CurrentDeadEndCorridor.GetComponent<SplitDecisionCorridor>();
        deadendCorridorScript.OnCorridorChosen += NextDecision;
        var progressCorridorScript = CurrentProgressionCorridor.GetComponent<SplitDecisionCorridor>();
        progressCorridorScript.OnCorridorChosen += NextDecision;

        //setup the traits for the newly created corridors
        deadendCorridorScript.Traits = new ObjectTraits(effectiveTraits.Colour, deadendDirection, effectiveTraits.Lighting);
        progressCorridorScript.Traits = new ObjectTraits(ineffectiveTraits.Colour, progressionDirection, ineffectiveTraits.Lighting);
    }

    void NextDecision()
    {
        //disable triggers so they cant trigger multiple times after choice was made
        CurrentDeadEndCorridor.GetComponent<BoxCollider>().enabled = false;
        CurrentProgressionCorridor.GetComponent<BoxCollider>().enabled = false;

        numberOfChoicesMade++;
        //bool to determine if we should build the end goal
        var isLastDecision = numberOfChoicesMade >= totalNumberOfDecisions;

        ObjectTraits predictedBestTraits;
        ObjectTraits predictedWorstTraits;
        //query logic agent to get next set of predictions and setup next decision to be made
        LogicAgent.Instance.CalculatePlayerPreferrences(out predictedBestTraits, out predictedWorstTraits);
        SetupDecision(NextDecisionCorridor, predictedBestTraits, predictedWorstTraits, isLastDecision);
    }
}
