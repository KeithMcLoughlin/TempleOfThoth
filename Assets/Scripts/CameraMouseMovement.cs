using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour {

    public float sensitivity = 5f;
    public float smoothing = 2f;
    public float maxVerticalRotation = 90f; //in degrees

    Vector2 lookingPosition;
    Vector2 smoothingVector;
    Vector2 mouseScalar;
    GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = this.transform.parent.gameObject;
        mouseScalar = new Vector2(sensitivity * smoothing, sensitivity * smoothing);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var mouseMovementInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var adjustedMouseMovement = Vector2.Scale(mouseMovementInput, mouseScalar);

        smoothingVector.x = Mathf.Lerp(smoothingVector.x, adjustedMouseMovement.x, 1f / smoothing);
        smoothingVector.y = Mathf.Lerp(smoothingVector.y, adjustedMouseMovement.y, 1f / smoothing);
        lookingPosition += smoothingVector;
        lookingPosition.y = Mathf.Clamp(lookingPosition.y, -maxVerticalRotation, maxVerticalRotation);

        transform.localRotation = Quaternion.AngleAxis(-lookingPosition.y, Vector3.right);
        player.transform.rotation = Quaternion.AngleAxis(lookingPosition.x, player.transform.up);
    }
}
