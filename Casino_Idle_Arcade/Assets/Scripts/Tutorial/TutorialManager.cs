using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public int startMoney;
    [SerializeField] float followCamTime;
    [SerializeField] float chipDeskTime;
    [SerializeField] Vector3 camOffset;
    float cashierTime;
    bool followArrow = true;
    bool changeCam = true;

    bool makeFirstRoulette;
    bool showCashier;
    bool spawnCustomers = true;
    bool getChip;
    public bool carryChip;

    bool showFirstLook;

    // clean
    bool goToCleanTable;
    bool baccaratGameIsEnded;

    [SerializeField] ArrowRenderer arrowRenderer;
    [SerializeField] GameObject standArrow;
    [SerializeField] GameObject followCam;
    [SerializeField] List<Transform> objs;
    [SerializeField] BuyAreaController baccaratBAController;
    [SerializeField] CashierManager cashierManager;
    [SerializeField] GameObject chipDesk;
    //[SerializeField] float beforeFollowTime;
    [SerializeField] Baccarat firstBaccarat;
    //[SerializeField] HandStack playerHandStack;
    [SerializeField] GameObject secondBaccarat;
    //[SerializeField] PriorityManager firstPriority_Baccarat;
    //[SerializeField] PlayerHandStackData playerHandStackData;
    [SerializeField] GameObject ambienceAudioSrc;

    [Header("Loot Start Money")]
    [SerializeField] StackMoney startMoneyStack;
    bool lootStartMoney;

    [Header("Upgrade Player stack")]
    [SerializeField] GameObject upgradeBtn;
    public bool canUpgradePlayerStack;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] ScrollRect upgradeRect;
    bool uiBlock_openUpgradePanel_onceOpen;
    [SerializeField] GameObject uiBlock_openUpgradePanel;
    [SerializeField] GameObject uiBlock_upgradePlayerStack;
    [SerializeField] GameObject uIBlock_closeUpgradePanel;
    [SerializeField] GameObject closeUpgradePanelBtn;
    float disableUpgradeRectCd = .1f;
    bool notYetUpgradePanelClosed;
    bool enoughMoneyToUpgradePlayerStack;

    // second bacc
    //[SerializeField] PriorityManager secondBaccaratPriority;
    //public List<GameObject> secondBaccPriorityObjs;
    //public GameObject secondBaccOpenedPriority;
        
    private void Awake()
    {
        if (!GameManager.isCompleteTutorial)
            cashierManager.gameObject.SetActive(false);
    }

    private void Start()
    {

        if (!GameManager.isCompleteTutorial)
        {
            cashierTime = cashierManager.data.cooldownAmount * firstBaccarat.maxGameCapacity + 5f;
            cashierManager.transform.parent.gameObject.SetActive(false);
            //getChipTime = playerHandStack.data.maxAddStackCd * playerHandStack.data.maxStackCount + 2f;
            //ChangeCamera();
            chipDesk.SetActive(false);
            
            standArrow.transform.DOMoveY(standArrow.transform.position.y - 1f, 1.5f).SetLoops(-1, LoopType.Yoyo);
            //firstPriority_Baccarat.priorityObjs.RemoveAt(0);
            cashierManager.transform.parent.gameObject.SetActive(false);
            firstBaccarat.transform.parent.gameObject.SetActive(false);
            baccaratBAController.defaultScale = 1;

            ambienceAudioSrc.SetActive(false);
            //upgradeBtn.SetActive(false);


            // second baccarat priority 
            //for (int i = 0; i < secondBaccaratPriority.priorityObjs.Count; i++)
            //{
            //    secondBaccPriorityObjs.Add(secondBaccaratPriority.priorityObjs[i]);
            //}
            //secondBaccOpenedPriority = secondBaccaratPriority.openedPriority;
            
            //secondBaccPriorityObjs.Add(secondBaccarat);
            //secondBaccOpenedPriority = firstBaccarat.gameObject;

        }
        else
        {
            cashierManager.transform.parent.gameObject.SetActive(true);
            standArrow.SetActive(false);
            ambienceAudioSrc.SetActive(true);
            upgradeBtn.SetActive(true);
        }
    }

    void Update()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = distanceFromScreen;

        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //arrowRenderer.SetPositions(
        //    PlayerMovements.Instance.transform.position, worldMousePosition);



        if (GameManager.isCompleteTutorial) return;
        
        MaxStackText.Instance.SetTextState(false);
        
        ShowFirstLook();
        LootStartMoney();
        MakeFirstBaccarat();
        ShowCashier();
        ChipDeskProcess();
        CarryChipToTable();
        ClearTable();
        //UpgradePlayerStack();
        SetArrowFollow();
    }


    void ShowFirstLook()
    {
        if (!showFirstLook)
        {
            showFirstLook = true;
            //ChangeCamera();
        }
    }
    void LootStartMoney()
    {
        if (!lootStartMoney && startMoneyStack.isPlayer)
        {
            objs.RemoveAt(0);
            changeCam = true;
            ChangeCamera();
            lootStartMoney = true;
            firstBaccarat.transform.parent.gameObject.SetActive(true);
        }


    }

    

    void MakeFirstBaccarat()
    {
        if (!makeFirstRoulette && baccaratBAController.price <= 0)
        {

            objs.RemoveAt(0);
            makeFirstRoulette = true;
            cashierManager.transform.parent.gameObject.SetActive(true);

            // it's active after tutorial, why after 0.01f is deactive? because we need to run start method of second bacc and childs
            StartCoroutine(DeactiveSeconBacc());


            changeCam = true;            
            // next move
            showCashier = true;

            
            ChangeCamera();
        }
    }

    IEnumerator DeactiveSeconBacc()
    {
        yield return new WaitForSeconds(.1f);
        //yield return new WaitForEndOfFrame();
        secondBaccarat.SetActive(false);
    }

    void ShowCashier()
    {
        if (showCashier)
        {
            if (spawnCustomers)
            {
                CustomerSpawner.instance.SpawnCustomers();
                spawnCustomers = false;
            }

            if (cashierManager.workerCheker.isPlayerAvailable)
            {
                cashierTime -= Time.deltaTime;
                if (cashierTime <= 0)
                {
                    objs.RemoveAt(0);
                    showCashier = false;
                    changeCam = true;
                    
                    // next move
                    getChip = true;
                    chipDesk.SetActive(true);

                    ChangeCamera();
                }
            }

        }
    }

    void ChipDeskProcess()
    {
        if (getChip && !PlayerMovements.Instance.handStack.StackIsEmpty())
        {

            chipDeskTime -= Time.deltaTime;

            if (chipDeskTime <= 0)
            {
                objs.RemoveAt(0);
                getChip = false;
                carryChip = true;
                changeCam = true;
                ChangeCamera();
            }  
        }
    }



    void CarryChipToTable()
    {
        if (carryChip && PlayerMovements.Instance.handStack.elementStack)
        {
            carryChip = false;
            arrowRenderer.gameObject.SetActive(false);
            //GameManager.isCompleteTutorial = true;
            standArrow.SetActive(false);
            goToCleanTable = true;
        }
    }


    void ClearTable()
    {
        if (goToCleanTable && firstBaccarat.playCd <= 0)
        {
            arrowRenderer.gameObject.SetActive(true);
            standArrow.SetActive(true);
            goToCleanTable = false;
            baccaratGameIsEnded = true;
        }

        if (baccaratGameIsEnded && firstBaccarat.isClean)
        {
            arrowRenderer.gameObject.SetActive(false);
            standArrow.SetActive(false);
            baccaratGameIsEnded = false;
            canUpgradePlayerStack = true;

            secondBaccarat.SetActive(true);
            ambienceAudioSrc.SetActive(true);

            secondBaccarat.SetActive(true);

            // save data
            GameManager.isCompleteTutorial = true;
            SaveLoadController.Instance.SaveTutorialState();
            GameManager.SaveTotalMoney();
            
            //PriorityController.Instance.AddPriority(secondBaccPriorityObjs, secondBaccOpenedPriority);
        }

    }



    void UpgradePlayerStack()
    {
        // check enough money after intro tutorial
        if (canUpgradePlayerStack && GameManager.GetTotalMoney() >= PlayerMovements.Instance.handStack.data.GetUpgradeCost())
            enoughMoneyToUpgradePlayerStack = true;

        
        if (canUpgradePlayerStack && enoughMoneyToUpgradePlayerStack)
        {
            PlayerMovements.Instance.canMove = false;
            PlayerMovements.Instance.SetMovingAnimationState(false);
            

            // check user click on upgrade btn
            /* notYetUpgradePanelClosed: when we press to close upgrade main panel, 
                a few time spend to close so uiBlocks like openPanel and upgradeStack is active again
                so we declare a bool var to complete process in last if block*/
            if (upgradePanel.activeSelf == true || notYetUpgradePanelClosed)
            {
                uiBlock_openUpgradePanel.SetActive(false);
                uiBlock_upgradePlayerStack.SetActive(true);

                closeUpgradePanelBtn.SetActive(false); 

                // wait to load upgradeItem and after disable rect scroll to user cant scroll 
                disableUpgradeRectCd -= Time.deltaTime;
                if (disableUpgradeRectCd <= 0)
                    upgradeRect.enabled = false;
                
                // when user upgrade hand stack
                if (PlayerMovements.Instance.handStack.data.upgradeLevelCounter == 1)
                {
                    uiBlock_upgradePlayerStack.SetActive(false);
                    uIBlock_closeUpgradePanel.SetActive(true);
                    notYetUpgradePanelClosed = true;
                    closeUpgradePanelBtn.SetActive(true);

                    // when user close the upgrade panel
                    if (upgradePanel.activeSelf == false)
                    {
                        PlayerMovements.Instance.canMove = true;
                        upgradeRect.enabled = true;
                        uIBlock_closeUpgradePanel.SetActive(false);
                        canUpgradePlayerStack = false;
                        ambienceAudioSrc.SetActive(true);
                        
                        //secondBaccarat.SetActive(true);

                        //GameManager.isCompleteTutorial = true;
                        
                        // save data
                        //SaveLoadController.Instance.SaveTutorialState();
                        //GameManager.SaveTotalMoney();

                    }
                }

            }
            else
            {
                upgradeBtn.SetActive(true);
                if (!uiBlock_openUpgradePanel_onceOpen)
                {
                    uiBlock_openUpgradePanel_onceOpen = true;
                    uiBlock_openUpgradePanel.SetActive(true); 
                }
            }
            
        }
    }


    void SetArrowFollow()
    {
        if (followArrow)
            arrowRenderer.SetPositions(GetPlayerPos(), objs[0].position);
    }
    
    void ChangeCamera()
    {
        if (changeCam)
        {
            changeCam = false;
            StartCoroutine(ChangeCameraDelay());
        }
    }

    IEnumerator ChangeCameraDelay()
    {
        followCam.transform.position = objs[0].position + camOffset;
        Joystick.Instance.ResetJoystick();
        Joystick.Instance.gameObject.SetActive(false);
        Joystick.Instance.transform.GetChild(0).gameObject.SetActive(false); 
        followCam.SetActive(true);

        standArrow.transform.position = objs[0].position + new Vector3(0, 6, 0);
        LootMoneu_UI.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(followCamTime);
        followCam.SetActive(false);
        Joystick.Instance.gameObject.SetActive(true);
        Joystick.Instance.ResetJoystick();
        LootMoneu_UI.Instance.gameObject.SetActive(true);

    }

    Vector3 GetPlayerPos() => PlayerMovements.Instance.transform.position;

    //private void OnApplicationQuit()
    //{
    //    if (!GameManager.isCompleteTutorial)
    //    {
    //        SaveLoad_CasinoElement.Instance.DeleteCasinoElementDataFile();
    //        PriorityController.Instance.DeleteOpenedPriorityFile();         
    //        SaveLoad_Cashier.Instance.DeleteCashierData();
    //        GameManager.DeleteTotalMoneyFile();
    //        LevelUpSlider.Instance.data.ResetData();
    //    }
    //}

}
