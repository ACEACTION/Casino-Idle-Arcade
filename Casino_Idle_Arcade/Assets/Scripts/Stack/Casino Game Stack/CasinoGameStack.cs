using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class CasinoGameStack : ElementStack
{
   
    [SerializeField] CasinoGame_ChipGame game;
    public override void Start()
    {
        base.Start();
        
    }

    

    public override void AddToGameStack(CasinoResource resource)
    {
        base.AddToGameStack(resource);

        //resource.transform.SetParent(firsStack.transform.parent);

        JumpMoveResource(resource);
        SetResourceParent(resource.transform, firsStack.transform.parent);

        firsStack.position += new Vector3(0, data.stackYOffset, 0);        
    }

    public override void CompleteJumpMove(CasinoResource resource)
    {
        base.CompleteJumpMove(resource);
    }

    public override void GetResource()
    {
        base.GetResource();
        firsStack.position -= new Vector3(0, data.stackYOffset, 0);
    }

    public override void SetDeliverProcess()
    {
        base.SetDeliverProcess();
        if (StackIsEmpty() && game.chipDeliverer != null)
        {
            game.chipDeliverer.casinoGamesPoses.Add(game);
            print("miaw");

        }

    }
}
