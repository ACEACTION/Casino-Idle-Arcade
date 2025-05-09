using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect;
    [SerializeField] ParticleSystem cleanEffect;

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

    public void PlayMessCardSfx() => AudioSourceManager.Instance.PlayMessCardSfx();

    public void PlayDustEffect()
    {
        dustEffect.gameObject.SetActive(true);
        dustEffect.Play();
    }
    public void PlayCleanEffect()
    {
        cleanEffect.gameObject.SetActive(true);
        cleanEffect.Play();
    }
}
