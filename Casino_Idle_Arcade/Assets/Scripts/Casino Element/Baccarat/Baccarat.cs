using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Baccarat : CasinoGame
{
    //variables
    [SerializeField] float dealerCastTime;
    public float cleaningCd;
    public int betCounter;
    bool getBet = false;
    public bool hasChip;
    float getChipCd;

    int winnerIndex;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;
    public bool isWorkerAvailable = false;
    bool payedMoney = true;


    [SerializeField] RouletteData data;
    [SerializeField] Sweeper sweeper;
    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] Slider cleaningSlider;
    [SerializeField] ParticleSystem cleaningParticle;
    [SerializeField] CasinoGameStack gameStack;
    [SerializeField] Transform playChipSpot;
    CasinoResource chip;
    public List<CasinoResource> chipsOnBet = new List<CasinoResource>();
    [SerializeField] CasinoChipGame_UI roulette_ui;
    [SerializeField] Slider gameSlider;

    private void OnEnable()
    {
        //transform.DOShakeScale(1f, 0.5f);
        WorkerManager.roulettes.Add(this);
        WorkerManager.AddNewRoulettesToAvailableWorker(this);
    }

    private void Start()
    {
        Init();
        CasinoElementManager.roulettes.Add(this);
        CasinoElementManager.allCasinoElements.Add(this);

    }

    void Init()
    {
        cleaningSlider.minValue = -data.cleaningCdAmount;
        getChipCd = data.maxGetChipCd;
        data.playChipSpotDefaultPos = playChipSpot.localPosition;
        gameSlider.maxValue = data.dealerCastTimeAmount;
        ShakeRoulette();
        gameStack.SetMaxStackCount(upgradeIndex);
        cleaningCd = data.cleaningCdAmount;
        dealerCastTime = data.dealerCastTimeAmount;
    }

    public override void PlayGame()
    {
        base.PlayGame();

        //animation customers = playing card
        ActiveactCustomerAnimation();
        //setting dealer animation to idle
        workerCheker.worker?.ActiveActionAnim(false);

        if (workerCheker.isPlayerAvailable)
        {
            // CinemachineManager.instance.ZoomIn();
        }

        gameSlider.gameObject?.SetActive(true);
        gameSlider.value += Time.deltaTime;
        dealerCastTime -= Time.deltaTime;
        if (dealerCastTime <= 0)
        {
            //game ended
            ChoseWinner();
            PayMoney();
            //StartCoroutine(ResetGame());
            //CinemachineManager.instance.ZoomOut();
            gameSlider.gameObject.SetActive(false);
        }

    }

    void PayMoney()
    {
        if (payedMoney)
        {
            payedMoney = false;
            int moneyAmount = Random.Range(data.moneyAmountLevel[upgradeIndex],
                data.moneyAmountLevel[upgradeIndex] + 2);
            for (int i = 0; i < moneyAmount; i++)
            {
                moneyStacks[Random.Range(0, moneyStacks.Length)].MakeMoney();
            }
        }
    }
    void ChoseWinner()
    {
        if (choseWinnerPossible)
        {

            ;
            sweeper.MessCards();
            if (cleaner != null) cleaner.cleaningSpot.Add(rwc.transform);

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
        yield return new WaitForSeconds(data.giveChipsToWinnerDelay);
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

            if (cleaningCd <= 0)
            {

                CleanProcess();
            }
        }
    }

    public void CleanProcess()
    {
        if (cleaner != null)
            cleaner.cleaningSpot.Remove(rwc.transform);

        isClean = true;
        cleaningParticle.gameObject.SetActive(true);
        cleaningParticle.Play();
        cleaningSlider.gameObject.SetActive(false);
        sweeper.Sweep();
        StartCoroutine(ResetGame());
    }


    public override IEnumerator ResetGame()
    {
        if (isClean)
        {
            AudioSourceManager.Instance.PlayShineSfx();
            cleaningCd = data.cleaningCdAmount;
            dealerCastTime = data.dealerCastTimeAmount;
            choseWinnerPossible = true;
            actCustomerAnimation = false;
            hasChip = false;
            chip = null;
            getBet = false;
            betCounter = 0;
            playChipSpot.localPosition = data.playChipSpotDefaultPos;
            chipsOnBet.Clear();
            payedMoney = true;
            gameSlider.value = 0;
            cleaningSlider.value = -data.cleaningCdAmount;

            gameSlider.gameObject.SetActive(false);

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
        if ((workerCheker.isPlayerAvailable
            || workerCheker.isDealerAvailabe)
            && readyToPlay)
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
                    workerCheker.worker?.ActiveActionAnim(true);
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
                       , 2, 1, data.getChipDuration);
                    playChipSpot.localPosition += new Vector3(0, data.playChipSpotOffset, 0);
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

                getChipCd = data.maxGetChipCd;


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
                betCounter += customer.Bet(data.betUnitPrice);
            }

            betCounter /= 100;
            roulette_ui.SetChipPanelState(true);
            roulette_ui.SetChipTxt(betCounter.ToString());
        }
    }


    public override void UpgradeElements()
    {
        base.UpgradeElements();


        if (dealerCastTime <= 0)
        {
            CleanProcess();
            StartCoroutine(ResetGame());
            print("1");
        }
        else
        {
            print("2");
            readyToPlay = false;
            cleaningCd = data.cleaningCdAmount;
            dealerCastTime = data.dealerCastTimeAmount;
            choseWinnerPossible = true;
            actCustomerAnimation = false;
            //hasChip = false;
            //chip = null;
            //getBet = false;
            //betCounter = 0;
            //playChipSpot.localPosition = playChipSpotDefaultPos;
            //chipsOnBet.Clear();
            payedMoney = true;
            gameSlider.value = 0;
            cleaningSlider.value = -data.cleaningCdAmount;
            gameSlider.gameObject.SetActive(false);
            ShakeRoulette();
        }

        gameStack.SetMaxStackCount(upgradeIndex);
    }
    void ShakeRoulette()
    {
        transform.DOShakeScale(1f, 0.5f);
    }

}
