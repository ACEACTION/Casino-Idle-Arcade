using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CasinoFood : CasinoResource
{
    
    [SerializeField] CasinoFoodData snackData;
    public void MoveSnackToCustomer(Transform destination)
    {
        transform.DOShakeScale(0.3f, 0.5f).SetDelay(snackData.timeToTravel / 2);
        transform.DOJump(destination.position, snackData.jumpPower, 1, snackData.timeToTravel);

    }

    public override void ReleaseResource()
    {
        base.ReleaseResource();
        VendingMachinePool.Instance.OnReleaseSnack(this);
    }

}
