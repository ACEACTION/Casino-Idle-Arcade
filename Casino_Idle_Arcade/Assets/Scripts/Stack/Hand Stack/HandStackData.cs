using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

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


    public override void OnEnable()
    {
        base.OnEnable();
        //maxStackCount = defaultMaxStackCount;
    }

    public override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        upgradeValue = 0;
        maxStackCount = defaultMaxStackCount;
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

        SaveLoadSystem.LoadAes<UpgradeHandStackDataSlot>((data) =>
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
        UpgradeHandStackDataSlot upgradeDataSlot = new UpgradeHandStackDataSlot();
        upgradeDataSlot.upgradeLevelCounter = upgradeLevelCounter;
        upgradeDataSlot.upgradeValue = upgradeValue;

        SaveLoadSystem.SaveAes(upgradeDataSlot, dataFileName,
            (error) =>
            {
                Debug.Log(error);
            },
            (success) =>
            {
               // Debug.Log(success);
            });
    }
}

[System.Serializable]
public class UpgradeHandStackDataSlot
{
    public int upgradeLevelCounter;
    public int upgradeValue;
}
