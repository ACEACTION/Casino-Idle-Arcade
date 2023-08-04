using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JackPot : CasinoGame
{
    [SerializeField] JackpotData data;
    [SerializeField] Animator animator;
    bool canPlayAnim = true;
    float winProbability;
    bool endGame;

    public override void Start()
    {
        base.Start();

        delayToReset = data.afterGameDuration;

        CasinoElementManager.allCasinoElements.Add(this);
        CasinoElementManager.jackPots.Add(this);
        

        if (!CasinoManager.instance.availableElements.Contains(ElementsType.jackpot))
        {
            CasinoManager.instance.availableElements.Add(ElementsType.jackpot);
        }
        ShakeElement();
        SetWinProbability();
        SetCastTime();
    }
        
    void ShakeElement() => transform.DOShakeScale(1f, 0.5f);

    private void Update()
    {
        if(readyToUse)
        {
            PlayGame();
            castTime -= Time.deltaTime;
            if(castTime <= 0)
            {
                //end game
                EndGame();
                StartCoroutine(ResetGame());
            }
        }
    }


    bool CustomerIsWinning() => Random.value < winProbability;

   
    public override void PlayGame()
    {
        base.PlayGame();
        if (canPlayAnim)
        {
            canPlayAnim = false;
            animator.SetTrigger("isPlayed");
        }
        customers[0].SetPlayingJackPotAnimation(true);
    }
    public void DisableJackPotAnim()
    {
        animator.ResetTrigger("isPlayed");
    }

    public override IEnumerator ResetGame()
    {
        canPlayAnim = true;
        endGame = false;
        return base.ResetGame();

    }

    public void EndGame()
    {
        if (!endGame)
        {
            if (CustomerIsWinning())
            {
                customers[0].dontGoToChipDesk = true;
                customers[0].SetWinJackpot(data.afterGameDuration);                
            }
            else
            {
                //customers[0].LosePorccess();
                customers[0].SetLose(data.afterGameDuration);
            }
            customers[0].SetPlayingJackPotAnimation(false);
            CustomerPayedMoney();

            endGame = true;

            AudioSourceManager.Instance.StopAudioSrc();
            AudioSourceManager.Instance.PlayEndJackpotSfx();
        }
    }

    void CustomerPayedMoney()
    {
        int moneyAmount = data.moneyAmountUpgradeLevel[upgradeIndex];

        customers[0].PayMoney(stackMoney, moneyAmount, MoneyType.jackpotMoney);
        
    }

    public override void UpgradeElements()
    {
        base.UpgradeElements();
        SetCastTime();
        ShakeElement();
        SetWinProbability();
    }


    void SetCastTime()
    {
        castTimeAmount = data.playGameTime[upgradeIndex];
        castTime = castTimeAmount;
    }
    void SetWinProbability() => winProbability = data.winProbabilityUpgradeLevel[upgradeIndex];

    // event
    public void PlayJackpotSfx_Event()
    {        
        //AudioSourceManager.Instance.PlayJackpotSfx();
    }
}
