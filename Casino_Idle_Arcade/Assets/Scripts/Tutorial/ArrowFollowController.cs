using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollowController : MonoBehaviour
{
    [SerializeField] float followCamTime;
    [SerializeField] Vector3 camOffset;
    float cashierTime;

    bool followArrow = true;
    bool changeCam = true;

    bool makeFirstRoulette;
    bool showCashier;
    bool spawnCustomers = true;
    bool getChip;
    bool carryChip;


    [SerializeField] ArrowRenderer arrowRenderer;
    [SerializeField] GameObject followCam;
    [SerializeField] List<Transform> objs;
    [SerializeField] BuyAreaController rouletteBAController;
    [SerializeField] CashierManager cashierManager;
    [SerializeField] GameObject chipDesk;   
    [SerializeField] Roulette roulette;
    [SerializeField] HandStack playerHandStack;

    private void Start()
    {

        if (!GameManager.completeTutorial)
        { 
            cashierTime = cashierManager.cooldownAmount * roulette.maxGameCapacity + 2f;
            ChangeCamera();
            chipDesk.SetActive(false);
        }
    }

    void Update()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = distanceFromScreen;

        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //arrowRenderer.SetPositions(
        //    PlayerMovements.Instance.transform.position, worldMousePosition);

        if (GameManager.completeTutorial) return;

        MakeFirstRoulette();
        ShowCashier();
        ChipDeskProcess();
        EndTutorial();
        SetArrowFollow();
    }

    void MakeFirstRoulette()
    {
        if (!makeFirstRoulette && rouletteBAController.price <= 0)
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
            Joystick.Instance.ResetJoystick();
            Joystick.Instance.gameObject.SetActive(false);
            if (!playerHandStack.CanAddStack())
            {
                objs.RemoveAt(0);
                getChip = false;
                Joystick.Instance.gameObject.SetActive(true);
                carryChip = true;
                changeCam = true;
                ChangeCamera();
            }
        }
    }


    void EndTutorial()
    {
        if (carryChip && roulette.cleaningCd <= 0)
        {
            carryChip = false;
            arrowRenderer.gameObject.SetActive(false);
            GameManager.completeTutorial = true;            
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
