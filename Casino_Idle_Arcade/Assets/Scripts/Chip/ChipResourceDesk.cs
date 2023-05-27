using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class ChipResourceDesk : CasinoResourceDesk
{
    int maxChipIndex;
    int chipIndex;

    private void Start()
    {
        maxChipIndex = Enum.GetValues(typeof(ChipType)).Length - 1;        
        chipIndex = -1;
    }


    public override void AddResourceToStack(HandStack stack)
    {
        base.AddResourceToStack(stack);
        Chip chip = MakeChip(GetChipType());
        SetResourcePos(chip.transform);
        SetResourceParent(chip.transform, stack.firstStack.transform.parent);
        SetResourceLocalJump(chip.transform, stack.firstStack);

        AddToStackList(stack.stackList, chip);
        AddToStackList(stack.chipList, chip);

    }

    ChipType GetChipType()
    {
        chipIndex++;
        if (chipIndex > maxChipIndex) chipIndex = 0;

        return (ChipType)chipIndex;
    }


    Chip MakeChip(ChipType chipType)
    {
        switch (chipType)
        {
            case ChipType.red:
                return ChipPool.Instance.redChipPool.Get();
            case ChipType.green:
                return ChipPool.Instance.greenChipPool.Get();
            case ChipType.blue:
                return ChipPool.Instance.blueChipPool.Get();

        }

        return ChipPool.Instance.redChipPool.Get();
    }

}
