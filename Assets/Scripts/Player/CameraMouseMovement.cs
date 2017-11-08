﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMouseMovement : MonoBehaviour {

    public float sensitivity = 5f;
    public float smoothing = 2f;
    public float maxVerticalRotation = 90f; //in degrees
    public float interactDistance = 2.5f;
    public GameObject playerCursor;

    Vector2 lookingPosition;
    Vector2 smoothingVector;
    Vector2 mouseScalar;
    GameObject player;
    Ray interactIndicator;
    RaycastHit interactable;
    int interactableLayer;
    Image playerCursorImage;
    
	void Start ()
    {
        player = this.transform.parent.gameObject;
        mouseScalar = new Vector2(sensitivity * smoothing, sensitivity * smoothing);
        Cursor.visible = false;
        interactableLayer = LayerMask.GetMask("Interactable");
        playerCursorImage = playerCursor.GetComponent<Image>();
    }
	
	void Update ()
    {
        //Reference: https://www.youtube.com/watch?v=blO039OzUZc video by Holistic3d, published Apr 23, 2016. Accessed on October 4, 2017
        //for mouse moving camera and smoothing
        var mouseMovementInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var adjustedMouseMovement = Vector2.Scale(mouseMovementInput, mouseScalar);

        smoothingVector.x = Mathf.Lerp(smoothingVector.x, adjustedMouseMovement.x, 1f / smoothing);
        smoothingVector.y = Mathf.Lerp(smoothingVector.y, adjustedMouseMovement.y, 1f / smoothing);
        lookingPosition += smoothingVector;
        lookingPosition.y = Mathf.Clamp(lookingPosition.y, -maxVerticalRotation, maxVerticalRotation);

        transform.localRotation = Quaternion.AngleAxis(-lookingPosition.y, Vector3.right);
        player.transform.rotation = Quaternion.AngleAxis(lookingPosition.x, player.transform.up);


        interactIndicator.origin = this.transform.position;
        interactIndicator.direction = this.transform.forward;
        //checks if the player is close enough and facing an interactable
        if(Physics.Raycast(interactIndicator, out interactable, interactDistance, interactableLayer))
        {
            //change cursor to indicate that player can interact with object
            playerCursorImage.color = Color.green;
            //if player clicks interactable, execute its interact method
            if(Input.GetMouseButtonDown(0))
            {
                var interactScript = interactable.collider.GetComponent<IInteractable>();
                interactScript.Interact();
            }
        }
        else
        {
            //if not close to an interactable, keep cursor as red
            playerCursorImage.color = Color.red;
        }
    }
}
