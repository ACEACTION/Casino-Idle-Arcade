using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UI/LevelUp Data")]
public class LevelUpData : ScriptableObject
{
    public int lvlUpCounter;
    public int moneyAmountPerUnit;
    public int lvlUpCurrentUnit;
    public List<int> maxLvlUpUnit = new List<int>();

    public int GetMoneyAmountPerLevelUp() => moneyAmountPerUnit;

}
