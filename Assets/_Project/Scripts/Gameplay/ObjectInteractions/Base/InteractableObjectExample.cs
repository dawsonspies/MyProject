using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectExample : Interactable
{

    [SerializeField] private string interactionText;

    public override string GetInteractionText()
    {
        return interactionText;
    }

    public override void Interact(GameObject interactor)
    {
        //interact code
    }

    public override void OnInteractableStart()
    {
        //if i need a start variable
    }
}
