using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public static class GameManager 
{

 
    // settings
    public static bool sfx;
    public static bool music;

    // money
    static int totalMoney;
    const string totalMoneyDataPath = "totalMoney";

    // tutorial
    public static bool isCompleteTutorial;

    //methods
    public static void AddMoney(int amount)
    {
        totalMoney += amount;
        SaveTotalMoney();
    }


    public static void MinusMoney(int amount)
    {
        totalMoney -= amount;
        SaveTotalMoney();
    }

    public static int GetTotalMoney() => totalMoney;


    public static void SaveTotalMoney()
    {
        SaveLoadSystem.SaveAes(totalMoney, totalMoneyDataPath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });
    }

    public static void LoadTotalMoney(int defaultMoney)
    {
        SaveLoadSystem.LoadAes<int>((data) => {
           totalMoney = data;
        }, totalMoneyDataPath
        , (error) => {
            totalMoney = defaultMoney;
        }
        , (success) => { Debug.Log(success); });
    }

    public static void DeleteTotalMoneyFile()
    {
        SaveLoadSystem.DeleteFile(totalMoneyDataPath);
    }

}
