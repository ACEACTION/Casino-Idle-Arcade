using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chip : CasinoResource
{
    public ChipType chipType;

    private void OnEnable()
    {
        transform.localScale = new Vector3(.7f, .82f, .7f);
    }

    public override void ReleaseResource()
    {
        base.ReleaseResource();
        ChipPool.Instance.OnReleaseChip(this, chipType);
    }


}
