using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Shape { Cylinder, Sphere, Pyramid, Cuboid, Cube }
public enum Direction { Right, Left, Straight, Behind }
public enum Size { Big, Medium, Small }

public class ObjectTraits
{
    public Color Colour { get; set; }
    //public Shape Shape { get; set; }
    public Direction Direction { get; set; }
    public Size Size { get; set; }

    public ObjectTraits(Color colour, Direction direction, Size size)
    {
        Colour = colour;
        Direction = direction;
        Size = size;
    }
}
