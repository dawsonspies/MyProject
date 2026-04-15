using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLogScript : Interactable
{
    //TODO:
    //1) stop any voice logs playing DONE
    //2) delete any voice log objects currently in the players audio holder DONE
    //3) wait for animation delay DONE
    //4) tehn play audio
    //5) put current voice log object in the players audio holder
    //6) play a UI trigger to play the audio playing animation
    //7) play a UI trigger to play
    //8) start subtitles or something idrk
    //9) save audio to unlocked array (to display in collectibles menu)

    [Header("Customizability")]
    [SerializeField] private string voiceLogInfo;
    [SerializeField] private string voiceLogInfo2;
    [SerializeField] private string voiceLogAuthor;

    [SerializeField] private string interactionText;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float audioPlayDelay;
    [SerializeField] private Transform voiceLogHolder;
    [SerializeField] private UITriggerScript uiTriggerScript;
    [SerializeField] private bool interactedWith = false;

    public override string GetInteractionText()
    {
        return interactionText;
    }

    public override void Interact(GameObject interactor)
    {
        if(interactedWith)
            return; //dont let interact twice

        //0) mark as interacted with
        interactedWith = true;

        //1) stop any voice logs playing
        audioSource.Pause();

        //2) delete any voice log objects currently in the players audio holder
        foreach (Transform child in voiceLogHolder)
        {
            Destroy(child.gameObject);
        }

        //3) wait for animation delay
        Invoke(nameof(PlayAudio), audioPlayDelay);

        //5) put current voice log object in the players audio holder
        transform.parent = voiceLogHolder.transform;
        transform.localPosition = Vector3.zero;
    }

    void PlayAudio()
    {
        //4) tehn play audio
        audioSource.PlayOneShot(audioClip);

        //6) play a UI trigger to play the audio playing animation
        uiTriggerScript.TriggerVoiceLogPopup(voiceLogInfo, voiceLogInfo2, voiceLogAuthor, true);
    }

    public override void OnInteractableStart()
    {
        //if i need a start variable
    }
}
