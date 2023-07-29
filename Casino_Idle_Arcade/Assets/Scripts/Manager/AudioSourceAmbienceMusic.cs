using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceAmbienceMusic : MonoBehaviour
{
    public AudioSource audioScr;
    public static AudioSourceAmbienceMusic Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
