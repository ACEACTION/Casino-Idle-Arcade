using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

[CreateAssetMenu(menuName = "Data/WorkerData")]
public class WorkerData : UpgradeData<float>
{
    public float moveSpeed;
    public float defaultMoveSpeed;

    public override void PassValueToData(float v)
    {
        base.PassValueToData(v);
        moveSpeed = v;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //moveSpeed = defaultMoveSpeed;
    }

    public override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        upgradeValue = 0;
        moveSpeed = defaultMoveSpeed;
    }

    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    LoadDefaultValue();
    //}

    public override void ResetData()
    {
        LoadDefaultValue();
        base.ResetData();
    }
    public override void LoadData()
    {
        base.LoadData();

        SaveLoadSystem.LoadAes<UpgradeWorkerDataSlot>((data) =>
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
           // Debug.Log(success);
        });

    }

    public override void SaveData()
    {
        base.SaveData();
        UpgradeWorkerDataSlot upgradeDataSlot = new UpgradeWorkerDataSlot();
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
public class UpgradeWorkerDataSlot
{
    public int upgradeLevelCounter;
    public float upgradeValue;
}

