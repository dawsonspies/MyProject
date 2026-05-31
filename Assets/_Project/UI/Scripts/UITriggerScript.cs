using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITriggerScript : MonoBehaviour
{
    //this script is for like causing UI events to happen
    //such as:
    //playing HUD animations
    //audio playing animations
    //triggering ui to show up like interactions
    //and triggering ui to show up to show whos talking n stuff
    //also subtitles

    [Header("Voice Log UI")]
    [SerializeField] private GameObject voiceLogPopup;
    [SerializeField] private AnimationClip voiceLogPopupAnim;
    [SerializeField] private Transform voiceLogRoot;
    [SerializeField] private float popupDeleteDelay = 6f;
    [SerializeField] private GameObject voiceLogPopupObj;

    public void TriggerVoiceLogPopup(string voiceLogInfo, string voiceLogInfo2, string voiceLogAuthor, bool playAnim)
    {
        voiceLogPopupObj = Instantiate(voiceLogPopup, voiceLogRoot, false);
        RectTransform rect = voiceLogPopupObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -30);

        TextMeshProUGUI[] textObjs = voiceLogPopupObj.GetComponentsInChildren<TextMeshProUGUI>();

        textObjs[0].text = voiceLogAuthor;
        textObjs[1].text = voiceLogInfo;
        textObjs[2].text = voiceLogInfo2;

        Invoke(nameof(DeletePopup), popupDeleteDelay);
    }

    void DeletePopup()
    {
        GameObject.Destroy(voiceLogPopupObj);
    }

    /* subtitles...
    public void TriggerSubtitle(string subtitle)
    {
        
    }
    */

}
