using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoFoodResourceDesk : CasinoResourceDesk
{
    public List<Transform> snackPositions = new List<Transform>();
    public List<CasinoFood> snacks = new List<CasinoFood>();

    [SerializeField] int maxCapacity;

    public bool HasCapacity { get { return snacks.Count < maxCapacity; } }


    public override void AddResourceToStack(HandStack stack)
    {
        base.AddResourceToStack(stack);

        if (snacks.Count != 0)
        {
            CasinoFood food = snacks[snacks.Count - 1];
            snacks.Remove(food);
            SetResourcePos(food.transform);
            SetResourceParent(food.transform, stack.firstStack.transform.parent);
            SetResourceLocalJump(food.transform, stack.firstStack);

            AddToStackList(stack.stackList, food);
            AddToStackList(stack.vMachineList, food);
        }
    }


}
