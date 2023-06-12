using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerModel : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CustomerMovement customerMvment;
    [SerializeField] GameObject[] cards;

    public void SetCardsState(bool state)
    {
        foreach (GameObject card in cards)
        {
            card.SetActive(state);
        }
    }

    public Animator GetAnimator() => anim;

    public void SetWinningAniamtionEvent()
    {
        customerMvment.WinningAnimationEvent();
    }

    public void SetLosingAnimationEvent()
    {
        customerMvment.LosingAnimationEvent();
    }

    public void disablingWinnigAnim() =>  anim.SetBool("isWinning", false);
    

}
