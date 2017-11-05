using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Shape { Cylinder, Sphere, Pyramid, Cuboid, Cube }
public enum Position { Right, Left, Straight, Behind }
public enum Size { Big, Medium, Small }

public class ObjectTraits
{
    public Color Color { get; set; }
    //public Shape Shape { get; set; }
    public Position Position { get; set; }
    public Size Size { get; set; }

    public ObjectTraits(Color color, Position position, Size size)
    {
        Color = color;
        Position = position;
        Size = size;
    }
}
