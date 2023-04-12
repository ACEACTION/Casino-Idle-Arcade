using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JackPot : CasinoGame
{
    [Range(0, 1)]
    [SerializeField] float winProbAbility;
    
    private void Start()
    {
        CasinoElementManager.jackPots.Add(this);
        
        if (!CasinoManager.instance.availableElements.Contains(ElementsType.jackpot))
        {
            CasinoManager.instance.availableElements.Add(ElementsType.jackpot);
        }
    }

    private void Update()
    {
        if(readyToPlay)
        {
            PlayGame();
            castTime -= Time.deltaTime;
            if(castTime <= 0)
            {
                //end game
                EndGame();
                StartCoroutine( ResetGame());
            }
        }
    }


    bool CustomerIsWinning() => Random.value < winProbAbility;

   
    public override void PlayGame()
    {
        base.PlayGame();

        //customers[0].SetPlayingJackPotAnimation(true);
        customers[0].SetPlayingCardAnimation(true);
    }

    public override IEnumerator ResetGame()
    {
        return base.ResetGame();
    }

    public void EndGame()
    {
        if(CustomerIsWinning())
        {
            customers[0].SetWinningAnimation(true);
        }
        else
        {
            customers[0].SetLosingAnimation(true);
        }
        stacks[Random.Range(0, stacks.Length)].MakeMoney();
        
    }


}
