using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Roulette : CasinoGame
{
    //variables
    [SerializeField] ParticleSystem cleaningParticle;
    [SerializeField] Slider cleaningSlider;
    [SerializeField] float dealerCastTime;
    [SerializeField] float dealerCastTimeAmount;
    [SerializeField] float getChipDuration;
    bool canChangeCamera;
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
    [SerializeField] float giveChipsToWinnerDelay = 2f;

    int winnerIndex;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;
    public bool isClean = true;
    public bool isWorkerAvailable = false;

    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] RouletteWorkerCheker rwc;
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
    }

    void Init()
    {
        cleaningSlider.minValue = -cleaningCdAmount;
        getChipCd = maxGetChipCd;
        playChipSpotDefaultPos = playChipSpot.localPosition;
    }

    bool payedMoney = true;
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
            PayMoney();   
            StartCoroutine(ResetGame());


            if (workerCheker.isPlayerAvailable && !rwc.canChangeCamera)
            {
                rwc.canChangeCamera = true;
                CinemachineManager.instance.ChangeCam();
            }
        }

    }

    void PayMoney()
    {
        if (payedMoney)
        {
            payedMoney = false;
            stacks[Random.Range(0, stacks.Length)].MakeMoney();
        }
    }
    void ChoseWinner()
    {
        if (choseWinnerPossible)
        {

;
            sweeper.ResetingCardsPoisiton();
            if (cleaner != null) cleaner.cleaningSpot.Add(this.transform);

            choseWinnerPossible = false;
            winnerIndex = Random.Range(0, customers.Count);
            foreach (CustomerMovement customer in customers)
            {
                if (customers.IndexOf(customer) == winnerIndex)
                { 
                    customer.WinProccess();
                    StartCoroutine(GiveChipsToWinner(customer));
                }
                else
                    customer.LosePorccess();
            }
            isClean = false;
            cleaningSlider.gameObject.SetActive(true);
            cleaningSlider.transform.DOShakeScale(0.5f, 0.03f);


        }
    }

    IEnumerator GiveChipsToWinner(Customer customer)
    {
        yield return new WaitForSeconds(giveChipsToWinnerDelay);
        foreach (CasinoResource resource in chipsOnBet)
        {
            customer.stack.AddChipToStack(resource);
        }
    }

    public void Cleaning()
    {
        if (!isClean && (rwc.isCleanerAvailabe || workerCheker.isPlayerAvailable))
        {
            cleaningSlider.value = -cleaningCd;
            cleaningCd -= Time.deltaTime;
            if(cleaningCd <= 0)
            {
                if(cleaner != null)
                    cleaner.cleaningSpot.Remove(this.transform);

                isClean = true;
                cleaningParticle.gameObject.SetActive(true);
                cleaningParticle.Play();
                cleaningSlider.gameObject.SetActive(false);
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
            chipsOnBet.Clear();
            payedMoney = true;


            yield return base.ResetGame();

        }
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {


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
            || workerCheker.isDealerAvailabe) 
            && readyToPlay)
        {
            GetBetAmountFromCustomer();
            GetChipFromStack();

            if (hasChip)
            {

                castTime -= Time.deltaTime;
                if (workerCheker.isPlayerAvailable && rwc.canChangeCamera)
                {
                    rwc.canChangeCamera = false;
                    CinemachineManager.instance.ChangeCam();
                }
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

            roulette_ui.SetChipPanelState(true);
            roulette_ui.SetChipTxt(betCounter.ToString());
        }
    }



}