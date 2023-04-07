using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CustomerMovement : Customer
{
    Transform dir;
    [SerializeField] NavMeshAgent agent;
    public Transform destination;


    private void Update()
    {
        dir = destination;
        if (destination != null)
        {
            agent.SetDestination(destination.position);
            if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, dir.rotation, 0.1f);
                if (stack.HasStack())
                {
                    anim.SetBool("idlecarry", true);
                    anim.SetBool("walkcarry", false);
                }
                else
                {
                    anim.SetBool("idle", true);
                    anim.SetBool("isWalking", false);
                }
            }
        }
    }


    public void SetMove(Transform ts)
    {
        if (stack.HasStack())
        {
            anim.SetBool("walkcarry", true);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
        destination = ts;

    }
    public void WinProccess()
    {
        SetWinningAnimation(true);
    }
    public void LosePorccess()
    {
        SetLosingAnimation(true);
    }

    //should be edited after this to add more chipdesk from manager
    public void WinningAnimationEvent()
    {
        isLeaving = true;
        SetMove(ChipDeskManager.FindNearestChipDesk(transform).customerSpot);
    }
    public void LosingAnimationEvent()
    {
        isLeaving = true;
        SetMove(ExitPosition.instance.customerSpot);
    }


    public void Leave()
    {
        if (tableWinner)
        {
            SetMove(ChipDeskManager.FindNearestChipDesk(transform).customerSpot);
        }
        else
        {
            //if bar is open
            SetMove(ExitPosition.instance.customerSpot);

        }
    }

    public void settingIdleAnimationTrue()
    {
        anim.SetBool("idle", true);
    }

    public void disablingWinnigAnim()
    {
        anim.SetBool("isWinning", false);
    }

    public void disablePlayingAnim()
    {
        anim.SetBool("isPlayingCard", false);
    }
}
