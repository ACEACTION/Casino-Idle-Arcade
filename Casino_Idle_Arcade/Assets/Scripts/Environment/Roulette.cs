using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using DG.Tweening;

public class Roulette : CasinoGame
{
    //variables
    [SerializeField] float dealerCastTime;
    [SerializeField] float dealerCastTimeAmount;
    [SerializeField] float getChipDuration;
    bool hasChip;

    int winnerIndex;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;

    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] CasinoGameStack gameStack;
    [SerializeField] Transform playChipSpot;
    CasinoResource chip;

    private void Start()
    {
        CasinoElementManager.roulettes.Add(this);
    }

    public override void PlayGame()
    {
        
        base.PlayGame();

        //animation customers = playing card
        ActiveactCustomerAnimation();
        //setting dealer animation to idle
        workerCheker.worker.ActiveActionAnim(false);


        dealerCastTime -= Time.deltaTime;
        if(dealerCastTime <= 0)
        {
            //game ended
            ChoseWinner();
            StartCoroutine(ResetGame());

        }

    }

    void ChoseWinner()
    {
        if (choseWinnerPossible)
        {
            choseWinnerPossible = false;
            winnerIndex = Random.Range(0, customers.Count);
            foreach (CustomerMovement customer in customers)
            {
                if (customers.IndexOf(customer) == winnerIndex) 
                    customer.WinProccess();
                else 
                    customer.LosePorccess();
            }
        }
    }


    public override IEnumerator ResetGame()
    {

        dealerCastTime = dealerCastTimeAmount;
        choseWinnerPossible = true;
        actCustomerAnimation = false;
        hasChip = false;
        chip = null;
        return base.ResetGame();
    }
    void ActiveactCustomerAnimation()
    {
        if (!actCustomerAnimation)
        {
            actCustomerAnimation = true;
            foreach (CustomerMovement customer in customers)
            {
                customer.SetPlayingCardAnimation(true);
            }
        }
    }

    private void Update()
    {
        if((workerCheker.isPlayerAvailable 
            || workerCheker.isWorkerAvailable) 
            && readyToPlay && gameStack.CanGetResource())
        {

            GetChipFromStack();

            castTime -= Time.deltaTime;
            if(castTime <= 0)
            {
                PlayGame();
            }
            else
            {
                workerCheker.worker.ActiveActionAnim(true);
            }
        }

    }

    

    public void GetChipFromStack()
    {
        if (!hasChip)
        {
            hasChip = true;
            chip = gameStack.GetFromGameStack();
            if (chip)
            {
                chip.transform.SetParent(transform);
                chip.transform.DOLocalJump(
                    playChipSpot.localPosition
                   , 2, 1, getChipDuration);

            }
        }
    }

}
