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
        

        predicatedEffectiveTraits = new ObjectTraits(Color.red, Direction.Left, Lighting.Bright);
        predicatedIneffectiveTraits = new ObjectTraits(Color.blue, Direction.Right, Lighting.None);
    }
}