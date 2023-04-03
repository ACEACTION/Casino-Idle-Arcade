using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chip : CasinoResource
{
    public ChipType chipType;

    public override void ReleasResource()
    {
        base.ReleasResource();
        ChipPool.Instance.OnReleaseChip(this, chipType);
    }
}
