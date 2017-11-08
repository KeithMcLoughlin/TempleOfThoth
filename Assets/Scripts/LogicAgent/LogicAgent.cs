using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /*
        var preferredColour = data.PlayerMostPreferredColour.Equals(Color.clear) ? AlterableObjectManager.GetRandomColour() 
                                                                                 : data.PlayerMostPreferredColour;
        var leastPreferredColour = data.PlayerLeastPreferredColour.Equals(Color.clear) ? AlterableObjectManager.GetRandomColour() 
                                                                                       : data.PlayerLeastPreferredColour;
        */
        var preferredColour = Color.green;
        var leastPreferredColour = Color.red;
        predicatedEffectiveTraits = new ObjectTraits(preferredColour, Direction.Left, Size.Medium);
        predicatedIneffectiveTraits = new ObjectTraits(leastPreferredColour, Direction.Right, Size.Medium);
    }
}
