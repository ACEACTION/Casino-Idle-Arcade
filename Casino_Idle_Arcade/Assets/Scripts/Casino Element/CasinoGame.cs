using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoGame : CasinoElement
{
    public float castTime;
    public float castTimeAmount;
    public float delayToReset;
    public bool readyToUse;
    public CasinoGameMoneyStack[] moneyStacks;
    public CasinoGameStack gameStack;
    public StackMoney stackMoney;
    
    
    public override void CustomerHasArrived()
    {
        base.CustomerHasArrived();
 
        customerCounter++;
        readyToUse = customerCounter == maxGameCapacity;
    }

    public override void CustomerLeft()
    {
        base.CustomerLeft();
        customerCounter--;
    }


    public virtual void PlayGame()
    {

    }

    public virtual IEnumerator ResetGame()
    {
        readyToUse = false;
        yield return new WaitForSeconds(delayToReset);
        castTime = castTimeAmount;
        customers.Clear();
        customerCounter = 0;
        SetNullElementSpotsCustomer();
    }
    
}