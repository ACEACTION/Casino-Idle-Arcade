using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoFoodResourceDesk : CasinoResourceDesk
{

    public override void AddResourceToStack(HandStack stack)
    {
        base.AddResourceToStack(stack);

        CasinoResource food = VendingMachinePool.Instance.casinoFoodPool.Get();
        SetResourcePos(food.transform);
        SetResourceParent(food.transform, stack.firstStack.transform.parent);
        SetResourceLocalJump(food.transform, stack.firstStack);
        
        AddToStackList(stack.stackList, food);
        AddToStackList(stack.vMachineList, food);
    }


}
