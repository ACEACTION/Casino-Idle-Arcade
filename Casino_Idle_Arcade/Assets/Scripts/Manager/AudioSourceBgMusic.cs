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
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    private void Start()
    {
        //SetAudioSource(clip);        
    }
    
    public void SetAudioSource(AudioClip audioClip)
    {
        audioSource.clip = audioClip;

        if (GameManager.music)
            audioSource.Play();
        else
            audioSource.Pause();

    }

    public void SetAudioSource()
    {
        audioSource.clip = clip;

        if (GameManager.music)
            audioSource.Play();
        else
            audioSource.Pause();
    }
}
