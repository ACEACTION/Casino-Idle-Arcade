using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] AudioClip[] pickUpCashSfx;
    [SerializeField] AudioClip[] placeChipSfx;
    [SerializeField] AudioClip sweepSfx;
    [SerializeField] AudioClip buyItemSfx;
    [SerializeField] AudioClip cantBuyItemSfx;
    [SerializeField] AudioClip switchTabSfx;
    [SerializeField] AudioClip[] popSfxs;
    [SerializeField] AudioClip shineSfx;
    [SerializeField] AudioClip[] doorSfxs;
    [SerializeField] AudioClip upgradeWorkerSfx;

    [SerializeField] AudioClip receptionSfx;   
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

    public void PlayDoorSound() => PlaySfx(doorSfxs[Random.Range(0, doorSfxs.Length)]);
    public void PlayBuyItem() => PlaySfx(buyItemSfx);
    public void PlayCantBuyItem() => PlaySfx(cantBuyItemSfx);

    public void PlaySwitchTabIcon() => PlaySfx(switchTabSfx);

    public void PlayCashPickupSfx() => PlaySfx(pickUpCashSfx[Random.Range(0,pickUpCashSfx.Length)]);
    public void PlayPoPSfx(int index) => PlaySfx(popSfxs[index]);

    public void PlaySweepSfx() => PlaySfx(sweepSfx);

    public void PlayBuyAreaSfx() => PlaySfx(buyAreaSfx);

    public void PlayShineSfx() => PlaySfx(shineSfx);

    public void PlayUpgradeWorkerSfx() => PlaySfx(upgradeWorkerSfx);
    public void PlaySfx(AudioClip sfx) => audioScr.PlayOneShot(sfx);

}
