using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    List<Color> UsableColours = new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };

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
        }
    }

    void Awake()
    {
        Instance = this;
        colourInteractedAmounts = new Dictionary<Color, int>();
        //intialise colour dictionary with all viable colours and set to zero
        foreach(var colour in UsableColours)
        {
            colourInteractedAmounts.Add(colour, 0);
        }

        //intialise direction dictionary with all viable directions and set to zero
        directionPreferrences = new Dictionary<Direction, int>();
        foreach(Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            directionPreferrences.Add(direction, 0);
        }

        /* dummy data, not needed */
        foreach (var colour in UsableColours)
        {
            colourInteractedAmounts[colour] = 3;
        }
        colourInteractedAmounts[Color.green] = 10;
        colourInteractedAmounts[Color.red] = 0;

        foreach (Direction direction in System.Enum.GetValues(typeof(Direction)))
        {
            directionPreferrences[direction] = 3;
        }
        directionPreferrences[Direction.Left] = 6;
        directionPreferrences[Direction.Right] = 0;
        /* end of dummy data */
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

        Debug.Log("Incremented colour interactions for: " + traits.Colour + ". " +
            "It has now been interacted " + colourInteractedAmounts[traits.Colour] + " number of times");
    }
}
