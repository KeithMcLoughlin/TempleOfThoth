using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterableObjectManager : MonoBehaviour {

    //List<GameObject> alterableGameObjects = new List<GameObject>();
    //GameObject button1;
    //GameObject button2;
    ObjectTraits predicatedBestTraits;
    ObjectTraits predicatedWorstTraits;

    void Start ()
    {
        //temp predicition which we will get from agent after logic applied
        //predicatedBestTraits = new ObjectTraits(Color.green, Shape.Cube, Position.Left, Size.Medium, InteractableType.PickUp);
        //predicatedWorstTraits = new ObjectTraits(Color.red, Shape.Cube, Position.Right, Size.Medium, InteractableType.PickUp);



        //button1 = GameObject.Find("Button1");
        //button2 = GameObject.Find("Button2");

        //var button1Renderer = button1.GetComponent<Renderer>();
        //button1Renderer.material.SetColor("_Color", Color.green);

        //var button2Renderer = button2.GetComponent<Renderer>();
        //button2Renderer.material.SetColor("_Color", Color.red);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void IntialiseObjects(List<GameObject> objects)
    {

    }
}
