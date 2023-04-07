using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] AudioClip[] pickUpCashSfx;
    [SerializeField] AudioSource audioScr;

    //singletoon
    public static AudioSourceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayCashPickupSfx()
    {
        audioScr.PlayOneShot(pickUpCashSfx[Random.Range(0, pickUpCashSfx.Length)]);
    }
}
