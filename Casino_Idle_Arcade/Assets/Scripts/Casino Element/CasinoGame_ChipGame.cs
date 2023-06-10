using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CasinoGame_ChipGame : CasinoGame
{
    //variables
    public float playCd;
    public float cleaningCd;
    public bool isWorkerAvailable = false;

    [Header("Chip Process")]
    public int betCounter;
    int totalChipCounter;
    bool getBet = false;
    public bool hasChip;
    float getChipCd;
    public bool giveChipsToWinner;

    [Header("Cleaner")]
    public RouletteCleaner cleaner;
    public RouletteEmployeeChecker rec;
    public bool isClean = true;

    [Header("Win Process")]
    CustomerMovement winCustomer;
    int winnerIndex;
    bool actCustomerAnimation = false;
    bool choseWinnerPossible = true;
    bool payedMoney = true;


    [Header("References")]
    [SerializeField] RouletteData data;
    public Sweeper sweeper;
    public ChipDeliverer chipDeliverer;
    public EmployeeCheker employeeChecker;
    public Slider cleaningSlider;
    [SerializeField] ParticleSystem cleaningParticle;
    [SerializeField] Transform playChipSpot;
    CasinoResource chip;
    public List<CasinoResource> chipsOnBet = new List<CasinoResource>();
    [SerializeField] CasinoChipGame_UI roulette_ui;
    public Slider gameSlider;
    [SerializeField] CasinoChipGameCanvas casinoGameCanvas;

    public void Init()
    {
        cleaningSlider.value = 0;
        cleaningSlider.maxValue = data.cleaningCdAmount;
        getChipCd = data.maxGetChipCd + .5f;
        data.playChipSpotDefaultPos = playChipSpot.localPosition;
        SetPlayCd(upgradeIndex);
        gameSlider.maxValue = playCd;
        ShakeModel();
        gameStack.SetMaxStackCount(upgradeIndex);
        cleaningCd = data.cleaningCdAmount;
    }

    public virtual void PayMoney()
    {
        if (payedMoney)
        {
            payedMoney = false;
            int moneyAmount = totalChipCounter * (upgradeIndex + 1);

            //if (totalChipCounter >= 1 && totalChipCounter < 5) moneyAmount *= 2;
            if (totalChipCounter >= 5 && totalChipCounter < 10)  moneyAmount = (int)(moneyAmount * 1.5f);
            if (totalChipCounter >= 10 && totalChipCounter < 15) moneyAmount = (int)(moneyAmount * 2f);
            if (totalChipCounter >= 15) moneyAmount *= 3;
            customers[winnerIndex].PayMoney(stackMoney, moneyAmount, MoneyType.baccaratMoney);

        }
    }


    public void ChoseWinner()
    {
        if (choseWinnerPossible)
        {

            if (cleaner != null) cleaner.destinationPoinst.Add(rec.transform);
            //CallDeliverer();

            choseWinnerPossible = false;
            winnerIndex = Random.Range(0, customers.Count);
            foreach (CustomerMovement customer in customers)
            {
                if (customers.IndexOf(customer) == winnerIndex)
                {
                    winCustomer = customer;
                    winCustomer.WinProccess();
                    StartCoroutine(GiveChipsToWinnerProcess());
                }
                else
                    customer.LosePorccess();
            }
           
            isClean = false;
            //cleaningSlider.gameObject.SetActive(true);
            //casinoGameCanvas.OpenCleanPanel();
            
        }
    }

    public override void PlayGame()
    {
        base.PlayGame();

        StartCoroutine(ActiveactCustomerAnimation());
        //setting dealer animation to idle
        employeeChecker.employee?.ActiveActionAnim(false);
        //gameSlider.gameObject?.SetActive(true);
        gameSlider.value += Time.deltaTime;
        playCd -= Time.deltaTime;

        if (playCd <= 0)
        {
            //game ended
            ChoseWinner();
            PayMoney();
            //gameSlider.gameObject.SetActive(false);
            casinoGameCanvas.ClosePlayGamePanel();
        }
        else
        {
            casinoGameCanvas.OpenPlayGamePanel();
        }

    }


    public void Cleaning()
    {
        if (!isClean && giveChipsToWinner && (rec.isCleanerAvailabe || employeeChecker.isPlayerAvailable))
        {
            cleaningSlider.value += Time.deltaTime;
            cleaningCd -= Time.deltaTime;

            if (cleaningCd <= 0)
            {
                CleanProcess();
            }
        }
    }

    public virtual void CleanProcess()
    {
        if (cleaner != null)
            cleaner.destinationPoinst.Remove(rec.transform);

        isClean = true;
        cleaningParticle.gameObject.SetActive(true);
        cleaningParticle.Play();
        //cleaningSlider.gameObject.SetActive(false);
        casinoGameCanvas.CloseCleanPanel();
        sweeper.Sweep();
        StartCoroutine(ResetGame());
    }

    public void CallCleaner()
    {
        if (!isClean)
        {
            if (cleaner != null) cleaner.destinationPoinst.Add(rec.transform);
        }
    }

    public IEnumerator CallDeliverer()
    {
        yield return new WaitForEndOfFrame();
        if (gameStack.StackIsEmpty())
        {
            if (chipDeliverer != null) chipDeliverer.casinoGamesPoses.Add(this);
        }
    }


    public override IEnumerator ResetGame()
    {
        if (isClean)
        {
            AudioSourceManager.Instance.PlayShineSfx();
            cleaningCd = data.cleaningCdAmount;
            SetPlayCd(upgradeIndex);
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
            cleaningSlider.value = 0;
            giveChipsToWinner = false;
            //gameSlider.gameObject.SetActive(false);
            casinoGameCanvas.CloseChipPanel();
            cleaningSlider.value = 0;

            ResetTableGame();

            yield return base.ResetGame();

        }
        yield break;
    }

    public virtual void ResetTableGame() {}

    public IEnumerator ActiveactCustomerAnimation()
    {
        if (!actCustomerAnimation)
        {
            actCustomerAnimation = true;
            foreach (CustomerMovement customer in customers)
            {
                yield return new WaitForSeconds(Random.Range(0, 1f));
                customer.SetPlayingCardAnimation(true);
            }
        }
    }

    public IEnumerator GiveChipsToWinnerProcess()
    {
        yield return new WaitForSeconds(data.giveChipsToWinnerDelay);

        // we check isClean bool because if upgrade table, we will have bug(Clean slider open)
        if (!isClean)
        {
            giveChipsToWinner = true;
            casinoGameCanvas.OpenCleanPanel();
            GiveChipsToCustomer();
        }
    }

    void GiveChipsToCustomer()
    {
        foreach (CasinoResource resource in chipsOnBet)
        {
            winCustomer.stack.AddResourceToStack(resource);
        }        
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
                        //roulette_ui.SetChipPanelState(false);
                        casinoGameCanvas.CloseChipPanel();
                    }
                    else
                        roulette_ui.SetChipTxt(betCounter.ToString());

                }
                StartCoroutine(CallDeliverer());

                getChipCd = data.maxGetChipCd;


            }
        }
    }

    public void GetBetAmountFromCustomer()
    {
        if (!getBet)
        {
            getBet = true;
            foreach (Customer customer in customers)
            {
                betCounter += customer.Bet(data.betUnitPrice);
            }

            betCounter /= 100;            

            roulette_ui.SetChipTxt(betCounter.ToString());
            casinoGameCanvas.OpenChipPanel();
            totalChipCounter = betCounter;
        }
    }


    public override void UpgradeElements()
    {
        base.UpgradeElements();

        if (playCd <= 0)
        {      
            if (!giveChipsToWinner)
            {               
                GiveChipsToCustomer();                
            }

            CleanProcess();            
            StartCoroutine(ResetGame());
        }
        else
        {
            readyToUse = false;
            cleaningCd = data.cleaningCdAmount;
            SetPlayCd(upgradeIndex);
            choseWinnerPossible = true;
            actCustomerAnimation = false;
            payedMoney = true;
            gameSlider.value = 0;
            cleaningSlider.value = 0;
            //gameSlider.gameObject.SetActive(false);
            casinoGameCanvas.CloseCleanPanel();
            ShakeModel();
        }

        gameSlider.maxValue = playCd;

    }

    void SetPlayCd(int upgradeIndex) => playCd = data.GetMaxPlayCdLevel(upgradeIndex);

}
