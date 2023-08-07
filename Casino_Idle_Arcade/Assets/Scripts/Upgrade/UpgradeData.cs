using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class UpgradeData <T> : ScriptableObject
{
    public string upgradeName;
    public T upgradeValue;
    public int upgradeLevelCounter; 

    public List<UpgradeSlot<T>> upgradeSlots;

    [Header("SaveLoadFile")]
    [SerializeField] string dataFileName;
    public UpgradeDataSlot<T> upgradeDataSlot;

    public bool CanUpgrade()
        => upgradeLevelCounter < upgradeSlots.Count;

    public void SetUgradeValue()
    {
        upgradeValue = upgradeSlots[upgradeLevelCounter].upgradeValue;
        upgradeLevelCounter++;
        PassValueToData(upgradeValue);
        SaveData();
    }

    public virtual void PassValueToData(T v) { }


    public int GetUpgradeCost() => upgradeSlots[upgradeLevelCounter].upgradeCost;

    public virtual void OnDisable()
    {
        upgradeLevelCounter = 0;
    }

    public virtual void OnEnable()
    {        
        //upgradeLevelCounter = 0;
    }

    public virtual void LoadDefaultValue() => upgradeLevelCounter = 0;

    public void LoadData()
    {
        SaveLoadSystem.LoadAes<UpgradeDataSlot<T>>((data) =>
        {
            upgradeLevelCounter = data.upgradeLevelCounter;
            upgradeValue = data.upgradeValue;
            upgradeDataSlot.upgradeLevelCounter = upgradeLevelCounter;
            upgradeDataSlot.upgradeValue = upgradeValue;
            PassValueToData(upgradeValue);
        }, dataFileName
        , (error) => { 
            Debug.Log(error); 
            LoadDefaultValue();
        }
        , (success) => { Debug.Log(success); });        
    }

    void SaveData()
    {
        upgradeDataSlot = new UpgradeDataSlot<T>();
        upgradeDataSlot.upgradeLevelCounter = upgradeLevelCounter;
        upgradeDataSlot.upgradeValue = upgradeValue;

        SaveLoadSystem.SaveAes(upgradeDataSlot, dataFileName,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });
    }

    public virtual void ResetData()
    {
        upgradeLevelCounter = 0;        
        upgradeDataSlot = null;
        SaveLoadSystem.DeleteFile(dataFileName);
    }

}

[System.Serializable]
public class UpgradeSlot<T>
{
    public T upgradeValue;
    public int upgradeCost;
}

[System.Serializable]
public class UpgradeDataSlot<T>
{
    public int upgradeLevelCounter;
    public T upgradeValue;
}
