using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UI/LevelUp Data")]
public class LevelUpData : ScriptableObject
{
    public int lvlUpCounter;
    public int moneyAmountPerUnit;
    public int lvlUpCurrentUnit;
    public int totalMoney;

    public List<LvlUpSlot> maxLvlUpUnit = new List<LvlUpSlot>();

    public int GetMoneyAmountPerLevelUp() => maxLvlUpUnit[lvlUpCounter - 1].moneyAmount;

    public void ResetData()
    {
        lvlUpCurrentUnit = 0;
        lvlUpCurrentUnit = 0;
        totalMoney = 0;
    }
}

[System.Serializable]
public class LvlUpSlot
{
    public int lvlUnit;
    public int moneyAmount;
}
