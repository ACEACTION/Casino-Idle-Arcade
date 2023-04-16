using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] AudioClip[] pickUpCashSfx;
    [SerializeField] AudioClip[] placeChipSfx;
    [SerializeField] AudioClip receptionSfx;
    [SerializeField] AudioClip musicBg;
    [SerializeField] AudioClip rouletteGamePlaySfx;
    [SerializeField] AudioClip buyAreaSfx;

    public AudioSource audioScr;

    //singletoon
    public static AudioSourceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
    }

    public void PlayCashPickupSfx() => PlaySfx(buyAreaSfx);

    public void PlayBuyAreaSfx() => PlaySfx(buyAreaSfx);

    public void PlaySfx(AudioClip sfx) => audioScr.PlayOneShot(sfx);

}
