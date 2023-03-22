using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoGame : CasinoElement
{
    public bool readyToPlay;
    public override void CustomerHasArrived()
    {
        base.CustomerHasArrived();
 
        customerCounter++;
        readyToPlay = customerCounter == maxGameCapacity;
    }
}
