using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Roulette : CasinoGame_ChipGame
{
    
    [SerializeField] Animator anim;
    [SerializeField] RouletteSpiner[] gameBalls;
    [SerializeField] RouletteData data;
    [SerializeField] GameObject staticBalls;
    [SerializeField] GameObject animatedBalls;



    private void OnEnable()
    {
        WorkerManager.casinoGamesForCleaners.Add(this);
        WorkerManager.casinoGamesForDeliverer.Add(this);
        WorkerManager.AddNewCasinoGamesToAvailabeCleaners(this);
        WorkerManager.AddGamesToDeliverer(this);
    }

    private void Start()
    {
        Init();
        SetGameBalls(true, false);
        CasinoElementManager.roulettes.Add(this);
        CasinoElementManager.allCasinoElements.Add(this);
    }

    
    public override void PlayGame()
    {
        base.PlayGame();
        
        // roulette animation
        staticBalls?.SetActive(false);
        animatedBalls?.SetActive(true);
        anim.SetBool("isSpining", true);        

        if(playCd <= 0)
        {        
            // roulette animation set off
            staticBalls?.SetActive(true);
            animatedBalls?.SetActive(false);
            anim.SetBool("isSpining", false);

            foreach (CustomerMovement cs in customers)
            {
                cs.disablePlayingAnim();
            }

        }
    }

    public override void ResetTableGame()
    {
    
    }
    
    
    private void Update()
    {
        if((workerCheker.isPlayerAvailable 
            || workerCheker.isDealerAvailabe) 
            && readyToUse)
        {
            GetBetAmountFromCustomer();
            GetChipFromStack();

            if (hasChip)
            {
                castTime -= Time.deltaTime;
                if (castTime <= 0)
                {
                    PlayGame();
                }
                else
                {
                    workerCheker.employee?.ActiveActionAnim(true);
                }
            }
        }

        Cleaning();

    }


    public override void CleanProcess()
    {
        base.CleanProcess();
        sweeper.PlayDustEffect();
        sweeper.PlayCleanEffect();
    }


    public override void UpgradeElements()
    {
        base.UpgradeElements();

        if (playCd <= 0)
        {        
            SetGameBalls(true, false);
        }
        else
        {
            SetGameBalls(false, true);
        }

        gameStack.SetMaxStackCount(upgradeIndex);
        
        // roulette
        staticBalls?.SetActive(true);
        animatedBalls?.SetActive(false);
        anim.SetBool("isSpining", false);
    }
   

    void SetGameBalls(bool staticBallState, bool animatedBallsState)
    {
        staticBalls = gameBalls[upgradeIndex].staticBalls;
        staticBalls.SetActive(staticBallState);
        animatedBalls = gameBalls[upgradeIndex].animatedBalls;
        animatedBalls.SetActive(animatedBallsState);
    }

}
