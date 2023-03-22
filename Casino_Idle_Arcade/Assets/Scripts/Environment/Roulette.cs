using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Roulette : CasinoGame
{
    //variables
    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] float dealerCastTime;
    [SerializeField] float dealerCastTimeAmount;

    int winnerIndex;
    public bool hasChips;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;
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
            ResetTable();
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

    void ResetTable()
    {
        choseWinnerPossible = true;
        actCustomerAnimation = false;
        customers.Clear();
        foreach(CasinoElementSpot spots in elementSpots)
        {
            spots.customer = null;
        }
        customerCounter = 0;
        castTime = castTimeAmount;
        readyToPlay = false;
        dealerCastTime = dealerCastTimeAmount;
        

    }
    void ActiveactCustomerAnimation()
    {
        if (!actCustomerAnimation)
        {
            actCustomerAnimation = true;
            foreach (CustomerMovement customer in customers)
            {
                customer.anim.SetBool("isPlayingCard", true);
            }
        }
    }

    private void Update()
    {
        if((workerCheker.isPlayerAvailable || workerCheker.isWorkerAvailable) && readyToPlay && hasChips)
        {

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
}
