using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour {

    public float speed = 2f;

    Vector3 openPosition;
    Vector3 closePosition;
    bool open = true;

	// Use this for initialization
	void Start ()
    {
        openPosition = transform.position;
        closePosition = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(open)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
	}

    void OpenDoor()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
        if(transform.position.y >= openPosition.y)
        {
            //transform.position.y = openPosition.y;
            open = true;
        }
    }

    void CloseDoor()
    {
        transform.Translate(0f, -(speed * Time.deltaTime), 0f);
        if (transform.position.y <= closePosition.y)
        {
            //transform.position.y = closePosition.y;
            open = false;
        }
    }
}
