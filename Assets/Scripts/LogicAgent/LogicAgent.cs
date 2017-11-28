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
        
        //getting most and least interacted colours
        //this logic is subject to change
        var preferredColour = data.PlayerMostPreferredColour;
        var leastPreferredColour = data.PlayerLeastPreferredColour;
        
        predicatedEffectiveTraits = new ObjectTraits(preferredColour, Direction.Left, Size.Medium);
        predicatedIneffectiveTraits = new ObjectTraits(leastPreferredColour, Direction.Right, Size.Medium);
        Debug.Log("Predicted colour that will attract the player is: " + predicatedEffectiveTraits.Colour);
        Debug.Log("Predicted colour that will discourage the player is: " + predicatedIneffectiveTraits.Colour);

        Debug.Log("Predicted direction the player will go is: " + predicatedEffectiveTraits.Direction);
        Debug.Log("Predicted direction the player will least likely go is: " + predicatedIneffectiveTraits.Direction);
    }
}