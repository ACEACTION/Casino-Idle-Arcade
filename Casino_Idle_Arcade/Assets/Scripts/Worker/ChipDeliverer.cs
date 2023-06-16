using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public enum ChipDeliverState
{
    waiting, Delivering
}
public class ChipDeliverer : Worker
{

    public ChipDeliverState state;
    public Transform destination;
    bool canFollow = true;
    float removeCd = 1.5f;
    [SerializeField] float camStayCd;
    [SerializeField] Vector3 offSet;
    [SerializeField] HandStack handStack;
    [SerializeField] float stopingRadius;
    public bool isDelivering = true;
    public float waitingCd;
    [SerializeField] float waitingCdAmount;
    public bool playerMaxStackTxtState;
    public bool getPlayerMaxStackTxtState;


    [SerializeField] CapsuleCollider collider;
    public List<CasinoGame_ChipGame> casinoGames = new List<CasinoGame_ChipGame>();
    public List<CasinoGame_ChipGame> casinoGamesPoses = new List<CasinoGame_ChipGame>();

    [SerializeField] Transform closestChipDesk;

    public void SetHandStack(HandStack stack)
    {
        handStack = stack;
    }
    private void OnEnable()
    {
        transform.position = spawnPoint.position;
        WorkerManager.chipDeliverers.Add(this);
        WorkerManager.AddAvailableGamesToDeliverer();
        agent.speed = workerData.moveSpeed;
        waitingCd = waitingCdAmount;
        state = ChipDeliverState.waiting;
        ArrivedToFirstPosition();

       // CameraFollow.instance.SetDynamicFollow_BuyWorker(transform);

        foreach (var casinoGame in casinoGames)
        {
            StartCoroutine(casinoGame.CallDeliverer());
        }
    }


    public override void ArrivedToFirstPosition()
    {
        base.ArrivedToFirstPosition();
        
        anim.SetBool("isDelivering", true);
        agent.SetDestination(afterSpawnTransform.position);
        
        
    }

    private void Update()
    {
          if (!canWork)
            {
                if (canFollow)
                {
                    //CameraFollow.instance.firstFollowCamera.gameObject.SetActive(true);
                    //CameraFollow.instance.CamFollowDynamic(transform);

                    //if (!getPlayerMaxStackTxtState)
                    //{
                    //    getPlayerMaxStackTxtState = true;
                    //    playerMaxStackTxtState = MaxStackText.Instance.gameObject.activeSelf;
                    //}

                    //MaxStackText.Instance.gameObject.SetActive(false);
                    //camStayCd -= Time.deltaTime;
                    //if (camStayCd <= 0)
                    //{
                    //    MaxStackText.Instance.gameObject.SetActive(playerMaxStackTxtState);
                    //    canFollow = false;
                    //    CameraFollow.instance.firstFollowCamera.gameObject.SetActive(false);
                    //}
                }

                //we have to wait till worker arrives to first position
                if (Vector3.Distance(transform.position, afterSpawnTransform.position) <= agent.stoppingDistance)
                {
                    //worker arrives to first position
                    canWork = true;
                    anim.SetBool("isDelivering", false);

                }

            }

            if (canWork)
            {


                //check if deliverer waited for order
                if (state == ChipDeliverState.waiting)
                {
                    if (handStack.CanRemoveStack())
                    {
                        anim.SetBool("idlecarry", true);
                        anim.SetBool("walkcarry", false);
                    }
                    else anim.SetBool("isDelivering", false);



                    //check if there is any order
                    if (casinoGamesPoses.Count > 0)
                    {
                        //check if we already have enough chips in deliverers hand
                        if (handStack.GetStackCount() >= casinoGamesPoses[0].gameStack.GetMaxStackCount())
                        {
                            //directly move the deliverer to the table it needs
                            destination = casinoGamesPoses[0].transform;
                        }
                        else
                        {
                            //we should move the deliverer towards chipdesk first
                            destination = closestChipDesk;
                        }

                        state = ChipDeliverState.Delivering;
                    }
                }
                if (state == ChipDeliverState.Delivering)
                {
                    agent.SetDestination(destination.position);

                    if (handStack.CanRemoveStack())
                    {
                        anim.SetBool("walkcarry", true);
                    }
                    else
                    {
                        anim.SetBool("isDelivering", true);
                    }
                    anim.SetBool("idlecarry", false);

                    if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                    {
                        //deliverer arrives to destination

                        if (handStack.CanRemoveStack())
                        {
                            anim.SetBool("idlecarry", true);
                            //  anim.SetBool("walkcarry", false);
                        }
                        anim.SetBool("isDelivering", false);
                        anim.SetBool("walkcarry", false);

                        waitingCd -= Time.deltaTime;
                        if (waitingCd <= 0)
                        {
                            agent.speed = workerData.moveSpeed;
                            //time to move to the next position
                            if (destination == closestChipDesk)
                            {
                                destination = casinoGamesPoses[0].transform;
                                agent.SetDestination(destination.position);

                            }

                            else
                            {
                                destination = sweeperSpot;
                            }

                            waitingCd = waitingCdAmount;

                        }


                    }
                    if (casinoGamesPoses.Count > 0 && destination == casinoGamesPoses[0].transform)
                    {

                        if (Vector3.Distance(transform.position, closestChipDesk.position) >= 2f)
                        {
                            collider.enabled = false;
                        }


                        if (Vector3.Distance(handStack.transform.position, casinoGamesPoses[0].transform.position) < 2f)
                        {
                            collider.enabled = true;


                            agent.speed = workerData.moveSpeed;

                            waitingCd -= Time.deltaTime;

                            if (waitingCd <= 0)
                            {

                                waitingCd = waitingCdAmount;
                                destination = sweeperSpot;

                            }
                            removeCd -= Time.deltaTime;
                            if (removeCd <= 0)
                            {

                                removeCd = 1.5f;
                                casinoGamesPoses.RemoveAt(0);
                            }

                        }
                    }

                    if (destination == sweeperSpot)
                    {
                        if (Vector3.Distance(transform.position, sweeperSpot.position) <= 1f)
                        {
                            state = ChipDeliverState.waiting;
                            agent.speed = workerData.moveSpeed;

                            if (handStack.CanRemoveStack())
                            {
                                anim.SetBool("idlecarry", true);
                                anim.SetBool("walkcarry", false);
                            }
                            anim.SetBool("isDelivering", false);

                        }

                        // agent.SetDestination(destination.position);

                    }

                }
            }

        
     
    }

    


}

