using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

[CreateAssetMenu(menuName = "Data/UI/LevelUp Data")]
public class LevelUpData : ScriptableObject
{
    [SerializeField] string lvlUpFileName;
    public int lvlUpUnit;
    public int moneyAmountPerUnit;
    public int lvlUpCounter;
    public int lvlUpCurrentValue;
    public int totalMoney;

    public List<LvlUpSlot> maxLvlUpUnit = new List<LvlUpSlot>();

    int GetMoneyAmountPerLevelUp() => maxLvlUpUnit[lvlUpCounter - 1].moneyAmount;

    public void SetTotalMoney() => totalMoney += GetMoneyAmountPerLevelUp();
    public void LoadData()
    {
        SaveLoadSystem.LoadAes<LvlUpSaveData>((data) =>
        {
            lvlUpCounter = data.lvlUpCounter;
            lvlUpCurrentValue = data.lvlUpCurrentValue;
            totalMoney = data.totalMoney;
        }, lvlUpFileName
        , (error) => {
            Debug.Log(error);
            LoadDefaultValue();
        }
        , (success) => { Debug.Log(success); });
    }

    public void SaveData()
    {
        LvlUpSaveData saveData = new LvlUpSaveData(
            lvlUpCounter
            , lvlUpCurrentValue
            , totalMoney);
        SaveLoadSystem.SaveAes(saveData, lvlUpFileName,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });
    }

    void LoadDefaultValue()
    {
        lvlUpCurrentValue = 0;
        totalMoney = 0;
        lvlUpCounter = 0;
    }

    public void ResetData()
    {
        LoadDefaultValue();
        SaveLoadSystem.DeleteFile(lvlUpFileName);
    }

    private void OnDisable()
    {
        LoadDefaultValue();
    }

}

[System.Serializable]
public class LvlUpSlot
{
    public int maxLvlValue;
    public int moneyAmount;
}

[System.Serializable]
public class LvlUpSaveData
{
    public int lvlUpCounter;
    public int lvlUpCurrentValue;
    public int totalMoney;

    public LvlUpSaveData(int lvlUpCounter, int lvlUpCurrentValue, int totalMoney)
    {
        this.lvlUpCounter = lvlUpCounter;
        this.lvlUpCurrentValue = lvlUpCurrentValue;
        this.totalMoney = totalMoney;
    }
}

