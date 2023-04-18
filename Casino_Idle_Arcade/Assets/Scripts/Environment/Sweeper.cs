using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect;

    public Animator cardsAnim;

    public void Sweep()
    {
        cardsAnim.SetBool("isClean", true);
        cardsAnim.SetBool("isMessy", false);
    }

    public void MessCards()
    {
        cardsAnim.SetBool("isClean", false);
        cardsAnim.SetBool("isMessy", true);
    }


    public void PlayDustEffect()
    {
        dustEffect.gameObject.SetActive(true);
        dustEffect.Play();
    }

}
