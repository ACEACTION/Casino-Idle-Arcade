using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChipDeliverer : Cleaner
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] HandStack handStack;
    float waitingCd;
    [SerializeField] float waitingCdAmount;

    public bool isDelivering = true;
    public List<CasinoGame> casinoGames = new List<CasinoGame>();

    [SerializeField] Transform closestChipDesk;
    private void OnEnable()
    {

        WorkerManager.chipDeliverers.Add(this);
        WorkerManager.AddAvailableGamesToDeliverer();
        
    }


    private void Start()
    {
        agent.speed = workerData.moveSpeed;
        waitingCd = waitingCdAmount;
    }


    private void Update()
    {
        if (casinoGames .Count != 0)
        {
            if(handStack.GetStackCount() <= 0)
            {

                agent.SetDestination(closestChipDesk.position);
                anim.SetBool("isDelivering", true);

                if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                {
                    anim.SetBool("isDelivering", false);
                }
            }
            else if(handStack.GetStackCount() <= handStack.maxStackCount && handStack.GetStackCount() >=0)
            {
                waitingCd -= Time.deltaTime;
                if (waitingCd <= 0)
                {
                    anim.SetBool("isDelivering", true);
                    agent.SetDestination(casinoGames[0].transform.position);
                    if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                    {
                        anim.SetBool("isDelivering", false);

                        if (casinoGames[0].gameStack.GetStackCount() >=
                            casinoGames[0].gameStack.GetMaxStackCount())
                        {
                            casinoGames.RemoveAt(0);
                            anim.SetBool("isDelivering", true);
                            agent.SetDestination(sweeperSpot.position);
                            if (Vector3.Distance(transform.position, sweeperSpot.position) <= agent.stoppingDistance)
                            {
                                anim.SetBool("isDelivering", false);

                            }
                        }
                    }
                }
            }

        }

    }

    public void GoPickupChip()
    {
        agent.SetDestination(closestChipDesk.position);
        anim.SetBool("isDelivering", true);
        if (Vector3.Distance(transform.position, closestChipDesk.position) <= agent.stoppingDistance)
        {
            
            //arrived to chipdesk
            if(handStack.GetStackCount() >= handStack.maxStackCount)
            {
                //chips become full
                agent.SetDestination(casinoGames[0].transform.position);
                if (handStack.GetStackCount() > 0)
                {
                    //staying there till
                    if(casinoGames[0].gameStack.GetStackCount() >= casinoGames[0].gameStack.GetMaxStackCount())
                    {

                        agent.SetDestination(sweeperSpot.position);

                        if(Vector3.Distance(transform.position, sweeperSpot.position) <= agent.stoppingDistance)
                        {
                            anim.SetBool("isDelivering", false);

                        }
                    }
                }

            }

        }
    }
}
