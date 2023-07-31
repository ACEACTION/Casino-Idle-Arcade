using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Baccarat : CasinoGame_ChipGame
{

    [Header("Card Process")]
    [SerializeField] float givingCardCd;
    [SerializeField] float givingCardCdAmount;
    [SerializeField] bool givingCard;
    [SerializeField] Animator cardAnim;


    private void OnEnable()
    {
        //transform.DOShakeScale(1f, 0.5f);
        WorkerManager.casinoGamesForCleaners.Add(this);
        WorkerManager.casinoGamesForDeliverer.Add(this);
        WorkerManager.AddNewCasinoGamesToAvailabeCleaners(this);
        WorkerManager.AddGamesToDeliverer(this);
    }

    public override void Start()
    {
        base.Start();
        Init();
        CasinoElementManager.roulettes.Add(this);
        CasinoElementManager.allCasinoElements.Add(this);

        
    }    

    public override void PlayGame()
    {
        base.PlayGame();

        if (playCd <= 0)
        {            
            // baccarat
            foreach (CustomerMovement cs in customers)
            {
                cs.SetCustomerCardsActiveState(false);
                cs.SetPlayingCardAnimationState(false);
            }
        }

    }

    
    private void Update()
    {
        if ((employeeChecker.isPlayerAvailable
            || employeeChecker.isDealerAvailabe)
            && readyToUse)
        {
            GetBetAmountFromCustomer();
            GetChipFromStack();

            if (hasChip)
            {
                if (castTime <= 0)
                {
                    GiveCardToCustomers();
                    if (givingCardCd <= 0)
                    {
                        PlayGame();
                    }
                    else
                    {
                        givingCardCd -= Time.deltaTime;
                    }
                }
                else
                {
                    castTime -= Time.deltaTime;
                }
            }
        }

        Cleaning();
    }

    
    public override void ResetTableGame()
    {
        base.ResetTableGame();        
        givingCardCd = givingCardCdAmount;
        givingCard = false;

    }

    void GiveCardToCustomers()
    {
        if (!givingCard)
        {
            givingCard = true;
            employeeChecker.employee?.ActiveActionAnim(true);
            sweeper.MessCards();
            foreach(CustomerMovement cs in customers)
            {
                cs.SetCustomerCardsActiveState(true);
            }

        }
    }       


    public override void UpgradeElements()
    {
        base.UpgradeElements();

        gameStack.SetMaxStackCount(upgradeIndex);
    }
    

}