using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceAmbienceMusic : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public AudioSource audioScr;
    public static AudioSourceAmbienceMusic Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.01f);
        if (!GameManager.music)
        { 
            audioScr.Pause();             
        }
    }

}
