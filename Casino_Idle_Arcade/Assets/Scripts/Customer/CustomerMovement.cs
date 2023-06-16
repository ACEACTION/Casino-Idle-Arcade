using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class CustomerMovement : Customer
{
    [SerializeField] CustomerModel customerModel;
    
    Transform dir;
    [SerializeField] NavMeshAgent agent;


    public Transform destination;
    public bool fCustomer;
    int emojiIndex;
    public bool dontGoToChipDesk;

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
            anim.SetBool("idlecarry", false);
            anim.SetBool("walkcarry", true);
        }
        else
        {
            if (isLosing)
                SetSadWalkState(true);
            else
                anim.SetBool("isWalking", true);
        }
        destination = ts;

    }    

    public void SetWin(float winDuration, List<CasinoResource> chips)
    {
        SetPlayingCardAnimationState(false);
        StartCoroutine(WinProcess(winDuration, chips));
        StartCoroutine(ActiveModelWithDelay(ActiveWinModel));
    }


    IEnumerator WinProcess(float winDuration, List<CasinoResource> chips)
    {
        ShowHappy();
        //SetWinningAnimationState(true);
        SetWinningAnimationState(true);
        yield return new WaitForSeconds(winDuration);
        foreach (CasinoResource resource in chips)
        {
            stack.AddResourceToStack(resource);
        }
        //SetWinningAnimationState(false);
        SetWinningAnimationState(false);


        if (!dontGoToChipDesk)
        {
            isLeaving = true;
            isWinning = true;
            SetChipDesk();
            SetMove(chipDesk.customerSpot);
        }        

    }

    void ActiveWinModel()
    {
        customerModel.ActiveRichModel();
        customerModel.SetModelEffState(true);
    }   

    public void SetLose(float loseDuration)
    {
        isLosing = true;
        StartCoroutine(LoseProcess(loseDuration));
    }

    IEnumerator LoseProcess(float loseDuration)
    {
        ShowSad();
        SetLosingAnimation(true);
        StartCoroutine(ActiveModelWithDelay(ActivePoorModel));

        //check if customer has chance to go 
        yield return new WaitForSeconds(loseDuration);
        SetLosingAnimation(false);

        if (!GoToWendingMachine())        
        {
            isLeaving = true;
            SetMove(ExitPosition.instance.customerSpot);
            SetSadWalkState(true);
        }
    }

    

    void ActivePoorModel()
    {
        customerModel.ActivePoorModel();
        customerModel.SetModelEffState(true);
    }

    IEnumerator ActiveModelWithDelay(Action activeModel)
    {
        yield return new WaitForSeconds(.5f);
        activeModel();
    }

    void ShowHappy()
    {
        for (int i = 0; i < confetti.Length; i++)
        {
            confetti[i].gameObject.SetActive(true);
            confetti[i].Play();
        }

        emojiIndex = Random.Range(0, happyEmojies.Length);
        happyEmojies[emojiIndex].gameObject.SetActive(true);
        happyEmojies[emojiIndex].Play();
    }

    void ShowSad()
    {
        emojiIndex = Random.Range(0, sadEmojies.Length);
        sadEmojies[emojiIndex].gameObject.SetActive(true);
        sadEmojies[emojiIndex].Play();
    }

    public void SetWinJackpot(float winDuration)
    {
        StartCoroutine(WinJackpotProcess(winDuration));
        StartCoroutine(ActiveModelWithDelay(ActiveWinModel));
    }
    
    IEnumerator WinJackpotProcess(float winDuration)
    {
        ShowHappy();
        SetWinningAnimationState(true);
        yield return new WaitForSeconds(winDuration);
        SetWinningAnimationState(false);
        Leave();
    }


    public void LoseJackpot(float loseDuration)
    {
        StartCoroutine(LoseJackpotProcess(loseDuration));
        StartCoroutine(ActiveModelWithDelay(ActivePoorModel));
    }

    IEnumerator LoseJackpotProcess(float loseDuration)
    {
        ShowSad();
        SetLosingAnimation(true);
        yield return new WaitForSeconds(loseDuration);       
    }

    public void ExitCasino() => SetMove(ExitPosition.instance.customerSpot);

    public void Leave()
    {
        if (tableWinner)
        {
            SetMove(chipDesk.customerSpot);
        }
        else
        {
            //if bar is open
            SetMove(ExitPosition.instance.customerSpot);
        }
    }


    // event
    public void DisalbeJackPotPlayingAnim()
    {
        anim.SetBool("isPlayingJackPot", false);
    }

    public void ReleaseCustomer()
    {
        stack.ReleaseResources();
        anim.SetBool("idlecarry", false);
        anim.SetBool("walkcarry", false);
        anim.SetBool("sadWalking", false);
        anim.SetBool("isLosing", false);
        anim.Play("idle");

        isLosing = false;
        chipDesk = null;
        isWinning = false;
        dontGoToChipDesk = false;
        // deactive particles
        foreach (ParticleSystem emoji in sadEmojies)
        {
            emoji.gameObject.SetActive(false);
        }


        foreach (ParticleSystem emoji in happyEmojies)
        {
            emoji.gameObject.SetActive(false);
        }

        foreach (ParticleSystem emoji in confetti)
        {
            emoji.gameObject.SetActive(false);
        }


        CustomerPool.instance.OnReleaseCustomer(this);

    }

    public bool GoToWendingMachine()
    {
        SetDesireRate();
        vendingMachine = vendingMachineManager.CanSendCustomerToVendingMachine();
        if (CustomerWantsVendingMachine() && vendingMachine)
        {
            //there is vending machine available
            isLosing = false;
            vendingMachine.SendCustomerToElement(this);
            vendingMachine = null;
            return true;
        }
        else return false;
    }

}
