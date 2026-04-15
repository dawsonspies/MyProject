using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //have it set the parent gameobject to interactable layer #3

    public abstract void Interact(GameObject interactor);

    public abstract string GetInteractionText(); //will return something like "Press [E] to open door"

    protected virtual void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");

        OnInteractableStart();
    }

    public abstract void OnInteractableStart();
}