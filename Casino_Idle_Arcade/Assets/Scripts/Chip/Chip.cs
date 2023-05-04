using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chip : CasinoResource
{
    public ChipType chipType;

    public override void ReleaseResource()
    {
        base.ReleaseResource();
        ChipPool.Instance.OnReleaseChip(this, chipType);
    }


}
