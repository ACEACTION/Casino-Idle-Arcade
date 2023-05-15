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
        
        resource.transform.SetParent(firsStack.transform.parent);
        resource.transform.DOLocalJump(firsStack.localPosition, 2, 1,
            data.addResourceToStackTime).OnComplete(() =>
            {
                //resource.transform.DOShakeScale(0f, 0.0f)
                //.OnComplete(() => { resource.transform.DOScale(0.67f, 0.1f); });
                resource.transform.DOScale(0.67f, 0.1f);
                casinoResources.Add(resource);
            });

        firsStack.position += new Vector3(0, data.stackYOffset, 0);
    }



    public override void SetDeliverProcess()
    {
        base.SetDeliverProcess();
        if (StackIsEmpty() && game.chipDeliverer != null)
        {
            game.chipDeliverer.casinoGamesPoses.Add(game);
        }
    }
}
