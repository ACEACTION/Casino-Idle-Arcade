using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachine : CasinoGame
{
    private void Start()
    {
        CasinoElementManager.jackPots.Add(this);
    }
}
