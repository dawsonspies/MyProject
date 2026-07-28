using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VHSTapeScript : Interactable
{
    [SerializeField] private string interactionText;
    [SerializeField] private VideoClip tapeVideo;

    public override string GetInteractionText()
    {
        return interactionText;

    }

    public override void Interact(GameObject interactor)
    {
        //interact code
        PlayerInventory.HasVHSTape(true, tapeVideo);
        Debug.Log("Vhs tape");
        Destroy(gameObject);
    }

    public override void OnInteractableStart()
    {

    }
}
