using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

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

        // animation speed
        animationMoveSpeed = v;
        PlayerMovements.Instance.SetAnimationMoveSpeed(animationMoveSpeed);

        // player move speed
        if (upgradeLevelCounter == 0)
        {
            playerMoveSpeed = playerMoveSpeedUpgrades[upgradeLevelCounter];
        }
        else
        {
            playerMoveSpeed = playerMoveSpeedUpgrades[upgradeLevelCounter - 1];
        }

        PlayerMovements.Instance.SetPlayerMoveSpeed(playerMoveSpeed);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //animationMoveSpeed = defaultAnimationMoveSpeed;        
        //playerMoveSpeed = defaultPlayerMoveSpeed;
    }

    public override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        upgradeValue = 0;
        playerMoveSpeed = defaultPlayerMoveSpeed;
        animationMoveSpeed = defaultAnimationMoveSpeed;
    }

    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //}

    public override void ResetData()
    {
        LoadDefaultValue();
        base.ResetData();
    }
    public override void LoadData()
    {
        base.LoadData();
        SaveLoadSystem.LoadAes<UpgradePlayerDataSlot>((data) =>
        {
            upgradeLevelCounter = data.upgradeLevelCounter;
            upgradeValue = data.upgradeValue;
            PassValueToData(upgradeValue);
        }, dataFileName
        , (error) =>
        {
            Debug.Log(error);
            LoadDefaultValue();
        }
        , (success) => 
        {
            //Debug.Log(success); 
        });

    }

    public override void SaveData()
    {
        base.SaveData();
        UpgradePlayerDataSlot upgradeDataSlot = new UpgradePlayerDataSlot();
        upgradeDataSlot.upgradeLevelCounter = upgradeLevelCounter;
        upgradeDataSlot.upgradeValue = upgradeValue;

        SaveLoadSystem.SaveAes(upgradeDataSlot, dataFileName,
            (error) =>
            {
                Debug.Log(error);
            },
            (success) =>
            {
                //Debug.Log(success);
            });
    }
}

[System.Serializable]
public class UpgradePlayerDataSlot
{
    public int upgradeLevelCounter;
    public float upgradeValue;
}

