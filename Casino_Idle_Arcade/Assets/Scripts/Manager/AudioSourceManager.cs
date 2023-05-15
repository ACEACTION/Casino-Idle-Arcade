using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] AudioClip[] pickUpCashSfx;
    [SerializeField] AudioClip[] placeChipSfx;
    [SerializeField] AudioClip sweepSfx;
    [SerializeField] AudioClip popSfx;
    [SerializeField] AudioClip shineSfx;

    [SerializeField] AudioClip receptionSfx;
    [SerializeField] AudioClip musicBg;
    [SerializeField] AudioClip rouletteGamePlaySfx;
    [SerializeField] AudioClip buyAreaSfx;
    [SerializeField] AudioClip fushSfx;
    public AudioSource audioScr;

    //singletoon
    public static AudioSourceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
    }

    public void PlayFushSfx() => PlaySfx(fushSfx);
    public void PlayCashPickupSfx() => PlaySfx(pickUpCashSfx[Random.Range(0,pickUpCashSfx.Length)]);
    public void PlayPoPSfx() => PlaySfx(popSfx);

    public void PlaySweepSfx() => PlaySfx(sweepSfx);

    public void PlayBuyAreaSfx() => PlaySfx(buyAreaSfx);

    public void PlayShineSfx() => PlaySfx(shineSfx);

    public void PlaySfx(AudioClip sfx) => audioScr.PlayOneShot(sfx);

}
