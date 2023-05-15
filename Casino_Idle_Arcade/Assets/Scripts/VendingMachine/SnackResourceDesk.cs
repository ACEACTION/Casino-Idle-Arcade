using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackResourceDesk : CasinoResourceDesk
{

    public override void AddResourceToStack(HandStack stack)
    {
        base.AddResourceToStack(stack);

        CasinoResource snack = VendingMachinePool.Instance.snackPool.Get();
        SetResourcePos(snack.transform);
        SetResourceParent(snack.transform, stack.firstStack.transform.parent);
        SetResourceLocalMove(snack.transform, stack.firstStack);
        
        AddToStackList(stack.stackList, snack);
        AddToStackList(stack.vMachineList, snack);
    }


}
