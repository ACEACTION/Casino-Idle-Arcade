using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] float followCamTime;
    [SerializeField] float chipDeskTime;
    [SerializeField] Vector3 camOffset;
    float cashierTime;
    float getChipTime;
    bool followArrow = true;
    bool changeCam = true;

    bool makeFirstRoulette;
    bool showCashier;
    bool spawnCustomers = true;
    bool getChip;
    public bool carryChip;


    [SerializeField] ArrowRenderer arrowRenderer;
    [SerializeField] GameObject standArrow;
    [SerializeField] GameObject followCam;
    [SerializeField] List<Transform> objs;
    [SerializeField] BuyAreaController baccaratBAController;
    [SerializeField] CashierManager cashierManager;
    [SerializeField] GameObject chipDesk;   
    [SerializeField] Baccarat baccarat;
    [SerializeField] HandStack playerHandStack;
    [SerializeField] GameObject secondBaccarat;
    private void Start()
    {

        if (!GameManager.isCompleteTutorial)
        { 
            cashierTime = cashierManager.data.cooldownAmount * baccarat.maxGameCapacity + 2f;
            getChipTime = playerHandStack.maxAddStackCd * playerHandStack.maxStackCount + 2f;
            ChangeCamera();
            chipDesk.SetActive(false);
            
            standArrow.transform.DOMoveY(standArrow.transform.position.y - 2f, 1.5f).SetLoops(-1, LoopType.Yoyo);
            

        }
        else
        {
            standArrow.SetActive(false);
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

        MakeFirstBaccarat();
        ShowCashier();
        ChipDeskProcess();
        EndTutorial();
        SetArrowFollow();
    }

    void MakeFirstBaccarat()
    {
        if (!makeFirstRoulette && baccaratBAController.price <= 0)
        {
            objs.RemoveAt(0);
            makeFirstRoulette = true;
            changeCam = true;
            
            // next move
            showCashier = true;
            
            ChangeCamera();
        }
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
        if (getChip && playerHandStack.stackHasResource)
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



    void EndTutorial()
    {
        if (carryChip && PlayerMovements.Instance.handStack.casinoGameStack)
        {
            carryChip = false;
            arrowRenderer.gameObject.SetActive(false);
            GameManager.isCompleteTutorial = true;
            standArrow.SetActive(false);
            secondBaccarat.SetActive(true);
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
        followCam.SetActive(true);
        Joystick.Instance.ResetJoystick();
        Joystick.Instance.gameObject.SetActive(false);        
        standArrow.transform.position = objs[0].position + new Vector3(0, 4, 0);
        yield return new WaitForSeconds(followCamTime);
        followCam.SetActive(false);
        Joystick.Instance.gameObject.SetActive(true);
        Joystick.Instance.ResetJoystick();
    }

    Vector3 GetPlayerPos() => PlayerMovements.Instance.transform.position;

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
