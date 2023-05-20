using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData <T> : ScriptableObject
{
    public T upgradeValue;
    public int upgradeLevelCounter; 

    public List<UpgradeSlot<T>> upgradeSlots;    


    public bool CanUpgrade()
        => upgradeLevelCounter < upgradeSlots.Count;

    public void SetUgradeValue()
    {        
        upgradeValue = upgradeSlots[upgradeLevelCounter].upgradeValue;
        upgradeLevelCounter++;
        PassValueToData(upgradeValue);
    }

    public virtual void PassValueToData(T v) { }


    public int GetUpgradeCost() => upgradeSlots[upgradeLevelCounter].upgradeCost;

    public virtual void OnDisable()
    {
        upgradeLevelCounter = 0;
    }

}

[System.Serializable]
public class UpgradeSlot<T>
{
    public T upgradeValue;
    public int upgradeCost;
}
