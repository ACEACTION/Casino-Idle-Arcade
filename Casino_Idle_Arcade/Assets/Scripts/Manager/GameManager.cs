using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    public static int totalMoney;
    public static bool isCompleteTutorial;
    public static void AddMoney(int amount)
    {
        totalMoney += amount;
        Money_UI.Instance.SetMoneyTxt();
    }


    public static void BuyCostFunc(int price)
    {
        totalMoney -= (totalMoney * 4 / 100);
        price -= (totalMoney * 4 / 100);
    }


}
