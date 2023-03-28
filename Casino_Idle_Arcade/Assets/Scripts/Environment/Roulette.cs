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
    public float cleaningCd;
    public RouletteCleaner cleaner;
    [SerializeField] float cleaningCdAmount;
    [SerializeField] Sweeper sweeper;
    [SerializeField] int betUnitPrice;
    public int betCounter;
    bool getBet = false;
    bool hasChip;
    [SerializeField] float maxGetChipCd;
    float getChipCd;
    [SerializeField] float playChipSpotOffset;
    Vector3 playChipSpotDefaultPos;

    int winnerIndex;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;
    public bool isClean = true;
    public bool isWorkerAvailable = false;

    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] CasinoGameStack gameStack;
    [SerializeField] Transform playChipSpot;
    CasinoResource chip;
    List<CasinoResource> chipsOnBet = new List<CasinoResource>();
    [SerializeField] Roulette_UI roulette_ui;
    private void OnEnable()
    {
        WorkerManager.roulettes.Add(this);
        WorkerManager.AddNewRoulettesToAvailableWorker(this);
    }
    private void Start()
    {
        Init();

        CasinoElementManager.roulettes.Add(this);
        WorkerManager.roulettes.Add(this);
        WorkerManager.AddNewRoulettesToAvailableWorker(this);
    }

    void Init()
    {
        getChipCd = maxGetChipCd;
        playChipSpotDefaultPos = playChipSpot.localPosition;
        roulette_ui.SetFatherPanelState(false);
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
            roulette_ui.SetFatherPanelState(false);
            //game ended
            ChoseWinner();
            StartCoroutine(ResetGame());
        }

    }

    void ChoseWinner()
    {
        if (choseWinnerPossible)
        {            
            
            sweeper.ResetingCardsPoisiton();
            cleaner.cleaningSpot.Add(this.transform);
            choseWinnerPossible = false;
            winnerIndex = Random.Range(0, customers.Count);
            foreach (CustomerMovement customer in customers)
            {
                if (customers.IndexOf(customer) == winnerIndex) 
                    customer.WinProccess();
                else 
                    customer.LosePorccess();
            }
            isClean = false;


        }
    }

    public void Cleaning()
    {
        if (!isClean && isWorkerAvailable)
        {
            cleaningCd -= Time.deltaTime;
            if(cleaningCd <= 0)
            {
                cleaner.cleaningSpot.Remove(this.transform);
                isClean = true;
                sweeper.Sweep();
            }
        }
    }

    public override IEnumerator ResetGame()
    {
        if (isClean)
        {
            cleaningCd = cleaningCdAmount;
            dealerCastTime = dealerCastTimeAmount;
            choseWinnerPossible = true;
            actCustomerAnimation = false;
            hasChip = false;
            chip = null;
            getBet = false;
            betCounter = 0;
            playChipSpot.localPosition = playChipSpotDefaultPos;            

            yield return base.ResetGame();

        }
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!isClean)
            {
                isWorkerAvailable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWorkerAvailable = false;
        }

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
            && readyToPlay)
        {
            GetBetAmountFromCustomer();
            GetChipFromStack();

            if (hasChip)
            {
                roulette_ui.SetFatherPanelState(true);
                roulette_ui.SetPlayingGamePanelState(true);

                castTime -= Time.deltaTime;
                if (castTime <= 0)
                {
                    PlayGame();
                }
                else
                {
                    workerCheker.worker.ActiveActionAnim(true);
                }
            }
        }

        Cleaning();

    }


 
    public void GetChipFromStack()
    {
        if (!hasChip && gameStack.CanGetResource())
        {
            getChipCd -= Time.deltaTime;
            if (getChipCd <= 0)
            {
                chip = gameStack.GetFromGameStack();
                if (chip)
                {
                    chip.transform.SetParent(transform);
                    chip.transform.DOLocalJump(
                        playChipSpot.localPosition
                       , 2, 1, getChipDuration);
                    playChipSpot.localPosition += new Vector3(0, playChipSpotOffset, 0);
                    betCounter--;
                    chipsOnBet.Add(chip);
                
                    if (betCounter <= 0)
                    {
                        hasChip = true;
                        roulette_ui.SetFatherPanelState(false);
                        roulette_ui.SetChipPanelState(false);
                    }
                    else 
                        roulette_ui.SetChipTxt(betCounter.ToString());

                }

                getChipCd = maxGetChipCd;


            }


        }
    }

    void GetBetAmountFromCustomer()
    {
        if (!getBet)
        {
            getBet = true;
            foreach (Customer customer in customers)
            {
                betCounter += customer.Bet(betUnitPrice);
            }

            betCounter /= 100;

            roulette_ui.SetFatherPanelState(true);
            roulette_ui.SetChipPanelState(true);
            roulette_ui.SetPlayingGamePanelState(false);
            roulette_ui.SetChipTxt(betCounter.ToString());
        }
    }



}