using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoGame : CasinoElement
{
    public float castTime;
    public float castTimeAmount;
    
    public bool readyToPlay;
    public override void CustomerHasArrived()
    {
        base.CustomerHasArrived();
 
        customerCounter++;
        readyToPlay = customerCounter == maxGameCapacity;
    }

    public virtual void PlayGame()
    {

    }
}
