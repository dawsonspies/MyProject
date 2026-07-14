using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio(AudioClip sound)
    {
        if(audioSource != null && audioSource.isPlaying) {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
