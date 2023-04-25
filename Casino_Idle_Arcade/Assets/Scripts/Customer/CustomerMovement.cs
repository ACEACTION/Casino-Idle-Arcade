using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CustomerMovement : Customer
{
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
            anim.SetBool("isWalking", true);
        }
        destination = ts;

    }
    public void WinProccess()
    {
        ShowHappy();
        SetWinningAnimation(true);
    }
    public void LosePorccess()
    {
        emojiIndex = Random.Range(0, sadEmojies.Length);
        sadEmojies[emojiIndex].gameObject.SetActive(true);
        sadEmojies[emojiIndex].Play();

        SetLosingAnimation(true);
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

    public void WinJackpotProcess()
    {
        ShowHappy();
        SetWinningAnimation(true);
        Invoke("Leave", 4);
    }

    


    //should be edited after this to add more chipdesk from manager
    public void WinningAnimationEvent()
    {
        if (!dontGoToChipDesk)
        {
            isLeaving = true;
            isWinning = true;
            SetChipDesk();
            SetMove(chipDesk.customerSpot);
        }
    }
    public void LosingAnimationEvent()
    {
        isLeaving = true;
        SetMove(ExitPosition.instance.customerSpot);
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
        anim.Play("idle");
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

}
