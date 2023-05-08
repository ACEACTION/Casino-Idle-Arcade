using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    waiting, Delivering
}
public class ChipDeliverer : Cleaner
{

    public State state;
    public Transform destination;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] HandStack handStack;
    [SerializeField] float stopingRadius;
    [SerializeField] CapsuleCollider collider;
    public float waitingCd;
    [SerializeField] float waitingCdAmount;

    public bool isDelivering = true;
    public List<CasinoGame_ChipGame> casinoGames = new List<CasinoGame_ChipGame>();
    public List<CasinoGame_ChipGame> casinoGamesPoses = new List<CasinoGame_ChipGame>();


    [SerializeField] Transform closestChipDesk;
    private void OnEnable()
    {

        WorkerManager.chipDeliverers.Add(this);
        WorkerManager.AddAvailableGamesToDeliverer();
        agent.speed = workerData.moveSpeed;
        waitingCd = waitingCdAmount;
        state = State.waiting;

        foreach (var casinoGame in casinoGames)
        {
            casinoGame.CallDeliverer();
        }
    }


    private void Start()
    {

    }


    private void Update()
    {
        //check if deliverer waited for order
        if(state == State.waiting)
        {
            anim.SetBool("isDelivering", false);

            //check if there is any order
            if (casinoGamesPoses.Count > 0)
            {
                //check if we already have enough chips in deliverers hand
                if(handStack.GetStackCount() >= casinoGamesPoses[0].gameStack.GetMaxStackCount())
                {
                    //directly move the deliverer to the table it needs
                    destination = casinoGamesPoses[0].transform;
                }
                else
                {
                    //we should move the deliverer towards chipdesk first
                    destination = closestChipDesk;
                }

                state = State.Delivering;
            }
        }
        if(state == State.Delivering)
        {
            agent.SetDestination(destination.position);
            anim.SetBool("isDelivering", true);

            if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                //deliverer arrives to destination

                anim.SetBool("isDelivering", false);
                agent.speed = 0;
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
                if (casinoGamesPoses.Count > 0 &&  destination == casinoGamesPoses[0].transform)
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
                    casinoGamesPoses.RemoveAt(0);


                }
            }

                if (destination == sweeperSpot)
                {
                    if (Vector3.Distance(transform.position, sweeperSpot.position) <= 1f)
                    {
                        state = State.waiting;
                        agent.speed = workerData.moveSpeed;
                        anim.SetBool("isDelivering", false);

                    }

                // agent.SetDestination(destination.position);

                }

            }
        }




}

