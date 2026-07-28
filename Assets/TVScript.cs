using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVScript : Interactable
{
    [SerializeField] private string interactionText;
    [SerializeField] private GameObject screenObj;

    public override string GetInteractionText()
    {
        return interactionText;
    }

    public override void Interact(GameObject interactor)
    {
        //interact code
        if (PlayerInventory.HasVHSTape())
        {
            screenObj.GetComponent<VideoPlayer>().clip = PlayerInventory.GetVideoClip();
        }

        screenObj.GetComponent<VideoPlayer>().Play();
    }

    public override void OnInteractableStart()
    {

    }
}
