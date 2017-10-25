using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Button,
    PickUp
}

public interface Interactable{

    void Interact();
}
