using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : UpgradeData<float>
{

    public float moveSpeed;

    public override void PassValueToData(float v)
    {
        base.PassValueToData(v);
        moveSpeed = v;
    }


    //public float[] moveSpeedUpgradeLevel;
    //public int[] moveSpeedUpgradeLevelCosts;
    //public int upgradeLevelCounter;

    //public bool CanUpgradeMoveSpeed()
    //    => upgradeLevelCounter < moveSpeedUpgradeLevel.Length;

    //public void SetMoveSpeed()
    //{
    //    moveSpeed = moveSpeedUpgradeLevel[upgradeLevelCounter];
    //    upgradeLevelCounter++;
    //}

    //public int GetUpgradeCost() => moveSpeedUpgradeLevelCosts[upgradeLevelCounter];

}
