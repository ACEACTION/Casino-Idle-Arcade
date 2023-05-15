using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VendingMachineStack : ElementStack
{
    public override void AddToGameStack(CasinoResource resource)
    {
        base.AddToGameStack(resource);
        SetResourceParent(resource.transform, firsStack);
        JumpMoveResource(resource);       
        RotateResource(resource.transform);
    }

    public override void CompleteJumpMove(CasinoResource resource)
    {
        base.CompleteJumpMove(resource);
       // resource.ReleaseResource();
    }

    public override void SetDeliverProcess()
    {
        base.SetDeliverProcess();
        // deliver process
    }

}
