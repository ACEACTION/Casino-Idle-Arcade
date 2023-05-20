using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Gramaphone : MonoBehaviour
{
    [SerializeField] int musicCost;
    [SerializeField] AudioClip[] casinoMusics;
    [SerializeField] ParticleSystem musicVfx;
    [SerializeField] Image bgImage;
    [SerializeField] Slider slider;
    [SerializeField] float waitingCd;
    [SerializeField] float waitingCdAmount;
    [SerializeField] Color enableColor;
    [SerializeField] Color disableColor;

    [SerializeField] bool isPlayerAvailbe;

    [SerializeField] Canvas gramaphoneUiCanvas;
    
    [SerializeField] float coldownToNextMusic;
    [SerializeField] float coldownToNextMusicAmount;
    [SerializeField] AudioSource audioSource;
    [SerializeField] TextMeshProUGUI musicCostTxt;
    [SerializeField] Transform playBtn;
    float playBtnScale;

    private void Start()
    {
        slider.maxValue = waitingCdAmount;
        musicCostTxt.text = musicCost.ToString();
        playBtnScale = playBtn.localScale.x;
    }


    void DisableMenu()
    {
        gramaphoneUiCanvas.gameObject.SetActive(false);
        bgImage.color = enableColor;

    }

    void EnableMenu()
    {
        gramaphoneUiCanvas.gameObject.SetActive(true);
        if(GameManager.totalMoney < musicCost)
        {
            bgImage.color = disableColor;
        }

    }

    


    private void Update()
    {
        
        if(isPlayerAvailbe)
        {
            slider.value += Time.deltaTime;
            waitingCd -= Time.deltaTime;
            if(waitingCd <= 0)
            {
                EnableMenu();
            }
        }
    }


    //THESE ARE BUTTON ACTION
    public void PlayVariation()
    {
        playBtn.localScale = new Vector3(playBtnScale, playBtnScale, playBtnScale);
        playBtn.DOScale(playBtnScale + .1f, .3f).OnComplete(() =>
        {
            playBtn.localScale = new Vector3(playBtnScale, playBtnScale, playBtnScale);
        });

        if (GameManager.totalMoney >= musicCost)
        {
            musicVfx.gameObject.SetActive(true);
            musicVfx.Play();
            // Current clip
            AudioClip clip = AudioSourceBgMusic.Instance.audioSource.clip;

            int randomIndex = Random.Range(0, casinoMusics.Length);
            // check current clip is not random clip
            if (casinoMusics[randomIndex].Equals(clip))
            {
                randomIndex++;
                if (randomIndex == casinoMusics.Length) randomIndex = 0;             
            }

            AudioSourceBgMusic.Instance.SetAudioSource(casinoMusics[randomIndex]);

            GameManager.totalMoney -= musicCost;
            Money_UI.Instance.SetTotalMoneyTxt();
            /*PlayMusic(casinoMusics[Random.Range]);
            GameManager.totalMoney -= index;*/

        }
    }

    public void PlayMusic(AudioClip sfx) => audioSource.PlayOneShot(sfx);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailbe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player has exit

            DisableMenu();
            //time to reset all coldowns
            isPlayerAvailbe = false;
            waitingCd = waitingCdAmount;
            slider.value = 0;

        }
    }
}
