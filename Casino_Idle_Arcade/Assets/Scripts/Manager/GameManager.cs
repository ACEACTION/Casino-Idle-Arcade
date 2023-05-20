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
    }
    

    public static void MinusMoney(int amount) => totalMoney -= amount;

}
