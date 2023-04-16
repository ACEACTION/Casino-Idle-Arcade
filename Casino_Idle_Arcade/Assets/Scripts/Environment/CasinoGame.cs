using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoGame : CasinoElement
{
    public float castTime;
    public float castTimeAmount;
    public float delayToReset;
    public bool readyToPlay;
    public CasinoGameMoneyStack[] stacks;

    public override void CustomerHasArrived()
    {
        base.CustomerHasArrived();
 
        customerCounter++;
        readyToPlay = customerCounter == maxGameCapacity;
    }

    public virtual void PlayGame()
    {

    }

    public virtual IEnumerator ResetGame()
    {
        readyToPlay = false;
        yield return new WaitForSeconds(delayToReset);
        castTime = castTimeAmount;
        customers.Clear();
        customerCounter = 0;
        SetNullElementSpotsCustomer();
    }

}
