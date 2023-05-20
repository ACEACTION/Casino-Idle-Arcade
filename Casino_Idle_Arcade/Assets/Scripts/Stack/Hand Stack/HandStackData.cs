using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stack/Hand Stack")]
public class HandStackData : UpgradeData<int>
{
    public int maxStackCount;

    public float stackYOffset;
    public float maxAddStackCd;
    public float maxRemoveStackCd;
    public Vector3 firsStackOrigin;

    public override void PassValueToData(int v)
    {
        base.PassValueToData(v);
        maxStackCount = v;
    }

    //public int[] maxStackCountUpgradeLevel;
    //public int[] upgradeLevelCosts;
    //public int upgradeLevelCounter;

    //public bool CanUpgradeStack()
    //    => upgradeLevelCounter < maxStackCountUpgradeLevel.Length;

    //public void SetMaxStackCount()
    //{
    //    maxStackCount = maxStackCountUpgradeLevel[upgradeLevelCounter];
    //    upgradeLevelCounter++;
    //}


    //public int GetUpgradeCost() => upgradeLevelCosts[upgradeLevelCounter];

}
