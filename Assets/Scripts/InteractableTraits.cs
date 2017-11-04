using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape { Cylinder, Sphere, Pyramid, Cuboid, Cube }
public enum Position { Right, Left, Straight, Behind }
public enum InteractableType { Button, PickUp }

public class InteractableTraits
{
    public Color Color { get; set; }
    public Shape Shape { get; set; }
    public Position Position { get; set; }
    public float Size { get; set; }
    public InteractableType Type { get; set; }

    public InteractableTraits(Color color, Shape shape, Position position, float size, InteractableType type)
    {
        Color = color;
        Shape = shape;
        Position = position;
        Size = size;
        Type = type;
    }
}
