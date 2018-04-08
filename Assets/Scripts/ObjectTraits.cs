using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Right, Left, Straight, Behind }
public enum Lighting { None, Dim, Normal, Bright }

public class ObjectTraits
{
    public Color Colour { get; set; }
    public Direction Direction { get; set; }
    public Lighting Lighting { get; set; }

    public ObjectTraits(Color colour, Direction direction, Lighting lighting)
    {
        Colour = colour;
        Direction = direction;
        Lighting = lighting;
    }
}
