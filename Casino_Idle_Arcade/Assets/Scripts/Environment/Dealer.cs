using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField] Animator anim;
    public void disablePlayingCardAnim()
    {
        anim.SetBool("playingCard", false);
    }
}
