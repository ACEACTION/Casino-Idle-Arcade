using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VendingMachineStack : ElementStack
{
    public override void AddToGameStack(CasinoResource resource)
    {
        base.AddToGameStack(resource);
        //resource.transform.SetParent(firsStack);
        resource.transform.SetParent(firsStack);
        resource.transform.DOJump(firsStack.position, 2, 1, data.addResourceToStackTime)
        .OnComplete(() =>
        {
            //casinoResources.Add(resource);
            resource.ReleaseResource();
        });

    }

    public override void SetDeliverProcess()
    {
        base.SetDeliverProcess();
        // deliver process
    }

}
