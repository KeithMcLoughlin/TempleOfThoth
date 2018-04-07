using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BeginningRoom : MonoBehaviour, ITrialRoom {

    GameObject Room;
    Color goalColour;
    string goalDirection;
    Color discouragedColour;
    string discouragedDirection;
    List<Door> beginningFrontDoors = new List<Door>();
    public bool completed = false;
    public Transform nextTrialPosition;

    List<Color> UsableColours = new List<Color> { Color.yellow, Color.red, Color.green, Color.blue, Color.black, Color.cyan, Color.magenta, Color.grey };
    Color GetRandomColour(IList<Color> selectableColours)
    {
        return selectableColours[Random.Range(0, selectableColours.Count - 1)];
    }

    //return the created room
    public GameObject Intialise(Transform position, ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        //Instantiate a prefab through code in C#. - Unity Answers [Internet]. [cited 2017 Nov 15]. 
        //Available from: https://answers.unity.com/questions/12003/instantiate-a-prefab-through-code-in-c.html
        //instantiate on instance of this rooms prefab from the resources folder
        Room = Instantiate(Resources.Load("BeginningRoomTemplate"), position) as GameObject;

        SetGoalPathAndDiscouragedPath(effectiveTraits, ineffectiveTraits);
        UsableColours.Remove(goalColour);
        UsableColours.Remove(discouragedColour);

        SetupDoorway("BackWall");
        SetupDoorway("FrontWall");
        SetupDoorway("LeftWall");
        SetupDoorway("RightWall");

        Debug.Log("Room Intialised");
        
        return Room;
    }

    void SetupDoorway(string Wall)
    {
        var wallTransform = Room.transform.Find(Wall);
        var doorwayCorridor = wallTransform.Find("DoorwayCorridor");
        var leftWall = wallTransform.Find("LeftDoorwayWall");
        var rightWall = wallTransform.Find("RightDoorwayWall");
        var halfCorridorWidth = doorwayCorridor.Find("Roof").localScale.z / 2;

        /* move the door between a range and adjust the walls either side of it */
        //pick random position for corridor
        doorwayCorridor.localPosition = new Vector3(doorwayCorridor.localPosition.x, doorwayCorridor.localPosition.y, Random.Range(rightWall.localPosition.z + halfCorridorWidth, leftWall.localPosition.z - halfCorridorWidth));
        //find the difference between the left walls position and corridors left side
        var leftWallToCorridor = leftWall.localPosition.z - (doorwayCorridor.localPosition.z + halfCorridorWidth);
        //move the point halfway between them
        leftWall.localPosition = new Vector3(leftWall.localPosition.x, leftWall.localPosition.y, leftWall.localPosition.z - (leftWallToCorridor / 2));
        //fix the scale to fill the left side
        leftWall.localScale = new Vector3(leftWall.localScale.x, leftWall.localScale.y, leftWallToCorridor);

        //find the difference between the right walls position and corridors right side
        var rightWallToCorridor = (doorwayCorridor.localPosition.z - halfCorridorWidth) - rightWall.localPosition.z;
        //move the point halfway between them
        rightWall.localPosition = new Vector3(rightWall.localPosition.x, rightWall.localPosition.y, rightWall.localPosition.z + (rightWallToCorridor / 2));
        //fix the scale to fill the right side
        rightWall.localScale = new Vector3(rightWall.localScale.x, rightWall.localScale.y, rightWallToCorridor);

        //set the predicted corridor the player will choose to the goal colour
        if(Wall.Equals(goalDirection))
        {
            Debug.Log("Setting up goal path which is: " + goalDirection + " with the colour " + goalColour);
            SetColourForCorridorDoors(goalColour, doorwayCorridor);
        }
        else if(Wall.Equals(discouragedDirection))
        {
            Debug.Log("Setting up discouraged path which is: " + discouragedDirection + " with the colour " + discouragedColour);
            SetColourForCorridorDoors(discouragedColour, doorwayCorridor);
        }
        else
        {
            var randomColour = GetRandomColour(UsableColours);
            SetColourForCorridorDoors(randomColour, doorwayCorridor);
            UsableColours.Remove(randomColour);
        }

        //subscribe to door trigger event so we know when to create next trial + add reference to list
        //so we can lock the doors when necessary
        /*var frontDoorway = doorwayCorridor.Find("FrontDoorway");
        var frontDoor = frontDoorway.Find("Door");
        beginningFrontDoors.Add(frontDoor.GetComponent<Door>());
        var doorscript = frontDoor.GetComponent<Door>();
        doorscript.OnDoorTriggered += TrialCompleted;*/
        
        var corridorScript = doorwayCorridor.GetComponent<Corridor>();
        corridorScript.OnCorridorEntered += TrialCompleted;
    }

    void SetColourForCorridorDoors(Color colourToSet, Transform corridor)
    {
        //set the colour for the front door
        var frontDoorway = corridor.Find("FrontDoorway");
        frontDoorway.Find("Door").GetComponent<Renderer>().material.SetColor("_Color", colourToSet);
        
        //set the colour for the back door
        var backDoorway = corridor.Find("BackDoorway");
        backDoorway.Find("Door").GetComponent<Renderer>().material.SetColor("_Color", colourToSet);
    }

    void SetGoalPathAndDiscouragedPath(ObjectTraits effectiveTraits, ObjectTraits ineffectiveTraits)
    {
        goalColour = effectiveTraits.Colour;
        switch(effectiveTraits.Direction)
        {
            case Direction.Left: { goalDirection = "LeftWall"; break; }
            case Direction.Right: { goalDirection = "RightWall"; break; }
            case Direction.Straight: { goalDirection = "FrontWall"; break; }
            case Direction.Behind: { goalDirection = "BackWall"; break; }
        }

        discouragedColour = ineffectiveTraits.Colour;
        switch (ineffectiveTraits.Direction)
        {
            case Direction.Left: { discouragedDirection = "LeftWall"; break; }
            case Direction.Right: { discouragedDirection = "RightWall"; break; }
            case Direction.Straight: { discouragedDirection = "FrontWall"; break; }
            case Direction.Behind: { discouragedDirection = "BackWall"; break; }
        }
    }

    void TrialCompleted(object sender)
    {
        Corridor corridor = sender as Corridor;

        completed = true;
        Debug.Log("begginging room completed");

        //get front door + lock it and backdoor position for new trial
        corridor.front.locked = true;
        nextTrialPosition = corridor.nextTrialPosition;
    }
}
