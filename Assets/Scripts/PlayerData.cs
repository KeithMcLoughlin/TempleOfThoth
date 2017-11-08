using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    Dictionary<Color, int> colourInteractedAmounts;
    public Color PlayerMostPreferredColour
    {
        get
        {
            return colourInteractedAmounts.FirstOrDefault(x => x.Value == colourInteractedAmounts.Values.Max()).Key;
        }
    }
    public Color PlayerLeastPreferredColour
    {
        get
        {
            var leastPreferredColour = colourInteractedAmounts.FirstOrDefault(x => x.Value == colourInteractedAmounts.Values.Min()).Key;
            if (leastPreferredColour.Equals(PlayerMostPreferredColour))
                return Color.clear;
            else
                return leastPreferredColour;
        }
    }

    Dictionary<Direction, int> directionPreferrences;
    public Direction? PlayerMostPreferredDirection
    {
        get
        {
            
            return directionPreferrences.FirstOrDefault(x => x.Value == directionPreferrences.Values.Max()).Key;
        }
    }
    public Direction? PlayerLeastPreferredDirection
    {
        get
        {
            return directionPreferrences.FirstOrDefault(x => x.Value == directionPreferrences.Values.Min()).Key;
            /*
            var leastPreferrerDirection = directionPreferrences.FirstOrDefault(x => x.Value == directionPreferrences.Values.Min()).Key;
            if (leastPreferredColour.Equals(PlayerMostPreferredColour))
                return Color.clear;
            else
                return leastPreferredColour;*/
        }
    }

    void Awake()
    {
        Instance = this;
        colourInteractedAmounts = new Dictionary<Color, int>();
        //intialise colour dictionary with all viable colours and set to zero
        foreach(var colour in AlterableObjectManager.UsableColours)
        {
            colourInteractedAmounts.Add(colour, 0);
        }

        //intialise direction dictionary with all viable directions and set to zero
        directionPreferrences = new Dictionary<Direction, int>();
        foreach(Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            directionPreferrences.Add(direction, 0);
        }
    }

    public void UpdateData(ObjectTraits traits)
    {
        int numOfTimesInteracted;
        //get the number of times colour was interacted with if it already exists in the dictionary
        if (colourInteractedAmounts.TryGetValue(traits.Colour, out numOfTimesInteracted))
            colourInteractedAmounts[traits.Colour] = ++numOfTimesInteracted;
        //add new colour with one interaction to the dictionary as it is new
        else
            colourInteractedAmounts.Add(traits.Colour, 1);
    }
}
