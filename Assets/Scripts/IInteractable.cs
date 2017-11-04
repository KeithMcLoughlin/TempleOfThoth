using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    InteractableTraits Traits { get; set; }
    void Interact();
}
