using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceBgMusic : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public static AudioSourceBgMusic Instance;
    private void Awake()
    {
         if (Instance == null) Instance = this;
    }

    private void Start()
    {
        SetAudioSource(clip);
    }

    public void SetAudioSource(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
