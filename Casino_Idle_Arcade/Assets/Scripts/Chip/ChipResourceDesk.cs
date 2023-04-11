using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class ChipResourceDesk : CasinoResourceDesk
{
    [SerializeField] ChipResourceDeskData data; 
    int maxChipIndex;
    int chipIndex;


    [SerializeField] Transform chipSpawnPoint;

    private void Start()
    {
        maxChipIndex = Enum.GetValues(typeof(ChipType)).Length - 1;        
        chipIndex = -1;
    }


    public override void AddResourceToStack(HandStack stack, List<CasinoResource> stackList)
    {
        base.AddResourceToStack(stack, stackList);
        Chip chip = MakeChip(GetChipType());
        //chip.transform.SetParent(chipSpawnPoint);
        chip.transform.SetParent(stack.firstStack.transform.parent);
        chip.transform.position = chipSpawnPoint.position;
        //chip.transform.localPosition = Vector3.zero;
        //chip.transform.localPosition = stack.firstStack.localPosition;
        chip.transform.DOLocalMove(stack.firstStack.localPosition, data.addResourceToDeskTime);                
        stackList.Add(chip);

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
