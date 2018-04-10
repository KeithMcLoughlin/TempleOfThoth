using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMouseMovement : MonoBehaviour {

    public float sensitivity = 5f;
    public float smoothing = 2f;
    public float maxVerticalRotation = 90f; //in degrees
    public float interactDistance = 2.5f;
    public float showIfLockedDistance = 15f;
    public GameObject playerCursor;

    Vector2 lookingPosition;
    Vector2 smoothingVector;
    Vector2 mouseScalar;
    GameObject player;
    Ray interactIndicator;
    RaycastHit interactable;
    int interactableLayer;
    Image playerCursorImage;
    public Sprite lockImage;
    public Sprite normalPlayerCursor;
    
	void Start ()
    {
        player = this.transform.parent.gameObject;
        mouseScalar = new Vector2(sensitivity * smoothing, sensitivity * smoothing);
        Cursor.visible = false;
        interactableLayer = LayerMask.GetMask("Interactable");
        playerCursorImage = playerCursor.GetComponent<Image>();
        interactIndicator = new Ray();
    }
	
	void Update ()
    {
        //Holistic3d. How to construct a simple First Person Controller with Camera Mouse Look in Unity 5[Internet]. [cited 2017 Oct 4].
        //Available from: https://www.youtube.com/watch?v=blO039OzUZc
        //for mouse moving camera and smoothing
        var mouseMovementInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var adjustedMouseMovement = Vector2.Scale(mouseMovementInput, mouseScalar);

        //calculate the smoothing required for the camera movement
        smoothingVector.x = Mathf.Lerp(smoothingVector.x, adjustedMouseMovement.x, 1f / smoothing);
        smoothingVector.y = Mathf.Lerp(smoothingVector.y, adjustedMouseMovement.y, 1f / smoothing);
        lookingPosition += smoothingVector;
        //prevent the player from being able to flip the camera on the y axis
        lookingPosition.y = Mathf.Clamp(lookingPosition.y, -maxVerticalRotation, maxVerticalRotation);

        //rotate the camera
        transform.localRotation = Quaternion.AngleAxis(-lookingPosition.y, Vector3.right);
        //rotate the player
        player.transform.rotation = Quaternion.AngleAxis(lookingPosition.x, player.transform.up);

        
        //setup the ray for determining if the player can interact with an interactable
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

        //check if the player is looking at a locked door
        RaycastHit door;
        if (Physics.Raycast(interactIndicator, out door, showIfLockedDistance) && door.transform.GetComponent<Door>() != null
            && door.transform.GetComponent<Door>().locked)
        {
            //check cursor to lock to indictate that the door is locked
            playerCursorImage.sprite = lockImage;
            playerCursorImage.transform.localScale = new Vector3(3, 3, 0);
        }
        else
        {
            //return it to normal if it isn't
            playerCursorImage.sprite = normalPlayerCursor;
            playerCursorImage.transform.localScale = new Vector3(1, 1, 0);
        }
    }
}
