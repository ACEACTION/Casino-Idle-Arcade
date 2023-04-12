using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    public static int totalMoney;
    public static bool completeTutorial;
    public static void AddMoney(int amount)
    {
        totalMoney += amount;
        Money_UI.Instance.SetMoneyTxt();
    }


}
