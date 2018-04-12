using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data;

public class LogicAgent : MonoBehaviour
{
    public static LogicAgent Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void CalculatePlayerPreferrences(out ObjectTraits predicatedEffectiveTraits, out ObjectTraits predicatedIneffectiveTraits)
    {
        var data = PlayerData.Instance;

        data.QuerySplitDecisionTrialData(0);

        predicatedEffectiveTraits = new ObjectTraits(Color.red, Direction.Left, Lighting.Bright);
        predicatedIneffectiveTraits = new ObjectTraits(Color.blue, Direction.Right, Lighting.None);
    }

    public void GetTrialBeginningInstructions(ITrialRoom trial, out ObjectTraits predicatedEffectiveTraits, out ObjectTraits predicatedIneffectiveTraits)
    {
        //inital initialising, will be assigned to based on trial name
        predicatedEffectiveTraits = new ObjectTraits(Color.red, Direction.Left, Lighting.Bright);
        predicatedIneffectiveTraits = new ObjectTraits(Color.blue, Direction.Right, Lighting.None);
        
        switch (trial.TrialName)
        {
            case "Split Decision Trial": { CalculateNextDecision(0, out predicatedEffectiveTraits, out predicatedIneffectiveTraits); break; }
        }
    }

    public void CalculateNextDecision(int decisionNum, out ObjectTraits predicatedEffectiveTraits, out ObjectTraits predicatedIneffectiveTraits)
    {
        var queryResults = PlayerData.Instance.QuerySplitDecisionTrialData(decisionNum);

        //traits have to be intialised at start but will be assigned the correct values further down
        Color effectiveColor = Color.white;
        Direction effectiveDirection = Direction.Right;
        Lighting effectiveLighting = Lighting.None;
        Color ineffectiveColor = Color.white;
        Direction ineffectiveDirection = Direction.Left;
        Lighting ineffectiveLighting = Lighting.None;

        //percentages to help calculate most chosen traits
        float mostDirectionPercent = 0.0f;
        float mostChosenLightingPercentage = 0.0f;
        float leastChosenLightingPercentage = 0.0f;
        float mostChosenColorPercentage = 0.0f;
        float leastChosenColorPercentage = 0.0f;

        List<DecisionDetails> differentUserGroupDecisionDetails = new List<DecisionDetails> {
            queryResults.AllUsers, queryResults.SameGenderUsers, queryResults.SameAgeGroupUsers, queryResults.SameNationalityUsers
        };

        foreach (var userGroup in differentUserGroupDecisionDetails)
        {
            var total = (float)userGroup.TotalChoices;
            //calculate most effective direction
            var rightDirectionPercentage = userGroup.RightChoiceCount / total;
            var leftDirectionPercentage = userGroup.LeftChoiceCount / total;
            var usersMostDirectionPercent = 0.0f;
            var usersMostChosenDirection = Direction.Left;

            if (rightDirectionPercentage > leftDirectionPercentage)
            {
                usersMostDirectionPercent = rightDirectionPercentage;
                usersMostChosenDirection = Direction.Right;
            }
            else
            {
                usersMostDirectionPercent = leftDirectionPercentage;
                usersMostChosenDirection = Direction.Left;
            }

            if (usersMostDirectionPercent > mostDirectionPercent)
            {
                mostDirectionPercent = usersMostDirectionPercent;
                effectiveDirection = usersMostChosenDirection;
            }

            //calculate most effective lighting
            var usersMostLightingPercentage = userGroup.CountOfMostPickedLighting / total;
            if (usersMostLightingPercentage > mostChosenLightingPercentage)
            {
                mostChosenLightingPercentage = usersMostLightingPercentage;
                effectiveLighting = userGroup.MostPickedLighting;
            }

            //calculate most ineffective lighting
            var usersLeastLightingPercentage = userGroup.CountOfLeastPickedLighting / total;
            if (usersLeastLightingPercentage > leastChosenLightingPercentage)
            {
                leastChosenLightingPercentage = usersLeastLightingPercentage;
                ineffectiveLighting = userGroup.LeastPickedLighting;
            }

            //calculate most effective color
            var usersMostColorPercentage = userGroup.CountOfMostPickedColour / total;
            if (usersMostColorPercentage > mostChosenColorPercentage)
            {
                mostChosenColorPercentage = usersMostColorPercentage;
                effectiveColor = ColorToEnumConverter.ColourEnumToColor(userGroup.MostPickedColour);
            }

            //calculate most ineffective color
            var usersLeastColorPercentage = userGroup.CountOfLeastPickedColour / total;
            if (usersLeastColorPercentage > leastChosenColorPercentage)
            {
                leastChosenColorPercentage = usersLeastColorPercentage;
                ineffectiveColor = ColorToEnumConverter.ColourEnumToColor(userGroup.LeastPickedColour);
            }
        }

        if (effectiveDirection == Direction.Right) ineffectiveDirection = Direction.Left;
        else ineffectiveDirection = Direction.Right;

        predicatedEffectiveTraits = new ObjectTraits(effectiveColor, effectiveDirection, effectiveLighting);
        predicatedIneffectiveTraits = new ObjectTraits(ineffectiveColor, ineffectiveDirection, ineffectiveLighting);
    }
}