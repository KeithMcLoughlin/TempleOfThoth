using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningRoom : MonoBehaviour, ITrialRoom {

    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject MiddleDoor;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject LeftGoalIndicator;
    public GameObject RightGoalIndicator;
    public GameObject MiddleGoalIndicator;

    Renderer leftGoalIndicatorRenderer;
    Renderer rightGoalIndicatorRenderer;
    Renderer middleGoalIndicatorRenderer;

    void Awake ()
    {
        leftGoalIndicatorRenderer = LeftGoalIndicator.GetComponent<Renderer>();
        rightGoalIndicatorRenderer = RightGoalIndicator.GetComponent<Renderer>();
        middleGoalIndicatorRenderer = MiddleGoalIndicator.GetComponent<Renderer>();
    }
    
    void Update ()
    {
		
	}

    public void IntialiseRoom(ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        var objects = new List<GameObject> { LeftDoor, RightDoor, MiddleDoor, LeftButton, RightButton };
        var colours = new List<Color>(AlterableObjectManager.UsableColours);

        //set all goal objects red at start
        leftGoalIndicatorRenderer.material.SetColor("_Color", Color.red);
        rightGoalIndicatorRenderer.material.SetColor("_Color", Color.red);
        middleGoalIndicatorRenderer.material.SetColor("_Color", Color.red);

        //based on predicted least effective direction, place goal there and make goal object green
        switch (ineffectiveTraits.Direction)
        {
            case Direction.Left:
            {
                LeftDoor.GetComponent<Renderer>().material.SetColor("_Color", ineffectiveTraits.Colour);
                leftGoalIndicatorRenderer.material.SetColor("_Color", Color.green);
                LeftButton.GetComponent<Renderer>().material.SetColor("_Color", ineffectiveTraits.Colour);
                objects.Remove(LeftDoor);
                objects.Remove(LeftButton);
                break;
            }
            case Direction.Right:
            {
                RightDoor.GetComponent<Renderer>().material.SetColor("_Color", ineffectiveTraits.Colour);
                rightGoalIndicatorRenderer.material.SetColor("_Color", Color.green);
                RightButton.GetComponent<Renderer>().material.SetColor("_Color", ineffectiveTraits.Colour);
                objects.Remove(RightDoor);
                objects.Remove(RightButton);
                break;
            }
            case Direction.Straight:
            {
                MiddleDoor.GetComponent<Renderer>().material.SetColor("_Color", ineffectiveTraits.Colour);
                middleGoalIndicatorRenderer.material.SetColor("_Color", Color.green);
                objects.Remove(MiddleDoor);
                break;
            }
        }

        //based on predicted most effective direction 
        switch (effectiveTraits.Direction)
        {
            case Direction.Left:
            {
                LeftDoor.GetComponent<Renderer>().material.SetColor("_Color", effectiveTraits.Colour);
                LeftButton.GetComponent<Renderer>().material.SetColor("_Color", effectiveTraits.Colour);
                objects.Remove(LeftDoor);
                objects.Remove(LeftButton);
                break;
            }
            case Direction.Right:
            {
                RightDoor.GetComponent<Renderer>().material.SetColor("_Color", effectiveTraits.Colour);
                RightButton.GetComponent<Renderer>().material.SetColor("_Color", effectiveTraits.Colour);
                objects.Remove(RightDoor);
                objects.Remove(RightButton);
                break;
            }
            case Direction.Straight:
            {
                MiddleDoor.GetComponent<Renderer>().material.SetColor("_Color", effectiveTraits.Colour);
                objects.Remove(MiddleDoor);
                break;
            }
        }

        colours.Remove(effectiveTraits.Colour);
        colours.Remove(ineffectiveTraits.Colour);
        //change the colour of the rest of the objects that havent been assigned a colour and make sure none repeat
        foreach(var obj in objects)
        {
            var randomColour = AlterableObjectManager.GetRandomColour(colours);
            obj.GetComponent<Renderer>().material.SetColor("_Color", randomColour);
            colours.Remove(randomColour);
        }
    }

    private class AlterableObject
    {
        GameObject alterableObject;
        Renderer alterableRenderer;
        bool goal = false;
    }
}
