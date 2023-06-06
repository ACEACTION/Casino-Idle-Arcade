using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : UpgradeData<float>
{
    [Header("Upgrade Values")]
    public float[] playerMoveSpeedUpgrades;

    [Header("Animation")]
    public float animationMoveSpeed;
    public float defaultAnimationMoveSpeed;

    [Header("Move speed")]
    public float playerMoveSpeed;
    public float defaultPlayerMoveSpeed;

    public override void PassValueToData(float v)
    {
        base.PassValueToData(v);
        animationMoveSpeed = v;
        // animation speed
        PlayerMovements.Instance.SetAnimationMoveSpeed(animationMoveSpeed);

        // player move speed
        PlayerMovements.Instance.SetPlayerMoveSpeed(playerMoveSpeedUpgrades[upgradeLevelCounter - 1]);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        animationMoveSpeed = defaultAnimationMoveSpeed;        
        playerMoveSpeed = defaultPlayerMoveSpeed;
    }

}
