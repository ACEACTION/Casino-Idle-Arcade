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
                StartCoroutine(GiveMoneyToCustomer());
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

    IEnumerator GiveMoneyToCustomer()
    {
        yield return new WaitForSeconds(data.afterGameDuration);
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = transform.position;
        customers[0].stack.AddResourceToStack(money);
    }

    void CustomerPayedMoney()
    {
        int moneyAmount = data.moneyAmountUpgradeLevel[upgradeIndex];

        customers[0].PayMoney(stackMoney, moneyAmount, MoneyType.jackpotMoney);
        
        /*for (int i = 0; i < moneyAmount; i++)
        {
            moneyStacks[Random.Range(0, moneyStacks.Length)].MakeMoney();
        }*/
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

    public void PlayJackpotSfx()
    {        
        AudioSourceManager.Instance.PlayJackpotSfx();
    }
}
