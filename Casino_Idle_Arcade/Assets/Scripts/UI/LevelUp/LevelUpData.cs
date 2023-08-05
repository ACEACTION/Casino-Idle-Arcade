using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UI/LevelUp Data")]
public class LevelUpData : ScriptableObject
{
    public int lvlUpUnit;
    public int moneyAmountPerUnit;
    public int lvlUpCounter;
    public int lvlUpCurrentValue;
    public int totalMoney;

    public List<LvlUpSlot> maxLvlUpUnit = new List<LvlUpSlot>();

    int GetMoneyAmountPerLevelUp() => maxLvlUpUnit[lvlUpCounter - 1].moneyAmount;

    public void SetTotalMoney() => totalMoney += GetMoneyAmountPerLevelUp();

    public void ResetData()
    {
        lvlUpCurrentValue = 0;
        lvlUpCurrentValue = 0;
        totalMoney = 0;
        lvlUpCounter = 0;
    }
}

[System.Serializable]
public class LvlUpSlot
{
    public int maxLvlValue;
    public int moneyAmount;
}
