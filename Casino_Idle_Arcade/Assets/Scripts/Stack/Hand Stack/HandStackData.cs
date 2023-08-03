using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stack/Hand Stack")]
public class HandStackData : UpgradeData<int>
{
    public int maxStackCount;
    public int defaultMaxStackCount;

    public float stackYOffset;
    public float maxAddStackCd;
    public float maxRemoveStackCd;
    [HideInInspector] public Vector3 firstStackOrigin;

    public override void PassValueToData(int v)
    {
        base.PassValueToData(v);
        maxStackCount = v;
    }

    public override void ResetData()
    {
        base.ResetData();
        upgradeValue = 0;
        maxStackCount = defaultMaxStackCount;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //maxStackCount = defaultMaxStackCount;
    }

}
