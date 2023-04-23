using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JackPot : CasinoGame
{
    [SerializeField] JackpotData data;
    
    float winProbability;
    
    private void Start()
    {
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
        if(readyToPlay)
        {
            PlayGame();
            castTime -= Time.deltaTime;
            if(castTime <= 0)
            {
                //end game
                EndGame();
                StartCoroutine( ResetGame());
            }
        }
    }


    bool CustomerIsWinning() => Random.value < winProbability;

   
    public override void PlayGame()
    {
        base.PlayGame();

        //customers[0].SetPlayingJackPotAnimation(true);
        customers[0].SetPlayingCardAnimation(true);
    }

    public override IEnumerator ResetGame()
    {
        return base.ResetGame();
    }

    public void EndGame()
    {
        if(CustomerIsWinning())
        {
            customers[0].SetWinningAnimation(true);
        }
        else
        {
            customers[0].SetLosingAnimation(true);
        }

        CustomerPayedMoney();        
    }

    void CustomerPayedMoney()
    {
        int moneyAmount = Random.Range(data.moneyAmountUpgradeLevel[upgradeIndex],
                data.moneyAmountUpgradeLevel[upgradeIndex] + 2);
        for (int i = 0; i < moneyAmount; i++)
        {
            moneyStacks[Random.Range(0, moneyStacks.Length)].MakeMoney();
        }
    }

    public override void UpgradeElements()
    {
        base.UpgradeElements();
        SetWinProbability();
        SetCastTime();
        ShakeElement();
    }


    void SetCastTime()
    {
        castTimeAmount = data.playGameTime[upgradeIndex];
        castTime = castTimeAmount;
    }
    void SetWinProbability() => winProbability = data.winProbabilityUpgradeLevel[upgradeIndex];

}
