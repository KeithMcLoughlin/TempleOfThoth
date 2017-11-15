using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningRoom : MonoBehaviour, ITrialRoom {

    //GameObject LeftDoor;
    //GameObject RightDoor;
    //GameObject FrontDoor;
    //GameObject BackDoor;
    //GameObject LeftButton;
    //GameObject RightButton;
    //GameObject LeftGoalIndicator;
    //GameObject RightGoalIndicator;
    //GameObject MiddleGoalIndicator;
    public GameObject Room;

    Renderer leftGoalIndicatorRenderer;
    Renderer rightGoalIndicatorRenderer;
    Renderer middleGoalIndicatorRenderer;

    void Awake ()
    {
        //leftGoalIndicatorRenderer = LeftGoalIndicator.GetComponent<Renderer>();
        //rightGoalIndicatorRenderer = RightGoalIndicator.GetComponent<Renderer>();
        //middleGoalIndicatorRenderer = MiddleGoalIndicator.GetComponent<Renderer>();
    }
    
    void Update ()
    {
		
	}

    //return the created room
    public GameObject Intialise(Vector3 position)
    {
        //https://answers.unity.com/questions/12003/instantiate-a-prefab-through-code-in-c.html, Accessed: 15/11/2017
        Room = Instantiate(Resources.Load("BeginningRoomTemplate"), position, new Quaternion()) as GameObject;
        
        SetupDoorway("BackWall");
        SetupDoorway("FrontWall");
        SetupDoorway("LeftWall");
        SetupDoorway("RightWall");

        return Room;
    }

    void SetupDoorway(string Wall)
    {
        var wallTransform = Room.transform.Find(Wall);
        var door = wallTransform.Find("DoorwayCorridor");
        var leftWall = wallTransform.Find("LeftDoorwayWall");
        var rightWall = wallTransform.Find("RightDoorwayWall");
        var halfCorridorWidth = door.Find("Roof").localScale.z / 2;

        //pick random position for corridor
        door.localPosition = new Vector3(door.localPosition.x, door.localPosition.y, Random.Range(rightWall.localPosition.z + halfCorridorWidth, leftWall.localPosition.z - halfCorridorWidth));
        //find the difference between the left walls position and corridors left side
        var leftWallToCorridor = leftWall.localPosition.z - (door.localPosition.z + halfCorridorWidth);
        //move the point halfway between them
        leftWall.localPosition = new Vector3(leftWall.localPosition.x, leftWall.localPosition.y, leftWall.localPosition.z - (leftWallToCorridor / 2));
        //fix the scale to fill the left side
        leftWall.localScale = new Vector3(leftWall.localScale.x, leftWall.localScale.y, leftWallToCorridor);

        //find the difference between the right walls position and corridors right side
        var rightWallToCorridor = (door.localPosition.z - halfCorridorWidth) - rightWall.localPosition.z;
        //move the point halfway between them
        rightWall.localPosition = new Vector3(rightWall.localPosition.x, rightWall.localPosition.y, rightWall.localPosition.z + (rightWallToCorridor / 2));
        //fix the scale to fill the right side
        rightWall.localScale = new Vector3(rightWall.localScale.x, rightWall.localScale.y, rightWallToCorridor);
    }

    public void IntialiseRoom(ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        //var objects = new List<GameObject> { LeftDoor, RightDoor, MiddleDoor, LeftButton, RightButton };
        var colours = new List<Color>(AlterableObjectManager.UsableColours);

        //set all goal objects red at start
        leftGoalIndicatorRenderer.material.SetColor("_Color", Color.red);
        rightGoalIndicatorRenderer.material.SetColor("_Color", Color.red);
        middleGoalIndicatorRenderer.material.SetColor("_Color", Color.red);

        //based on predicted least effective direction, place goal there and make goal object green
        /*switch (ineffectiveTraits.Direction)
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
        }*/
    }

    private class AlterableObject
    {
        GameObject alterableObject;
        Renderer alterableRenderer;
        bool goal = false;
    }
}
