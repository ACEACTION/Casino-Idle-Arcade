using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MoneyData")]
public class MoneyData : ScriptableObject
{

    public int receptionMoneyAmount;
    public int rouletteMoneyAmount;
    public int baccaratMoneyAmount;
    public int jackpotMoneyAmount;
    public int chipDeskMoneyAmount;
    public int barMoneyAmount;
    public float moneyGoToCustomerFromDeskTime;
    public float goToPlayerTime;

    public int GetMoneyAmount(MoneyType type)
    {
        switch (type)
        {
            case MoneyType.receptionMoney:
                return receptionMoneyAmount;
            case MoneyType.rouletteMoney:
                return rouletteMoneyAmount;
            case MoneyType.baccaratMoney:
                return baccaratMoneyAmount;
            case MoneyType.jackpotMoney:
                return jackpotMoneyAmount;
            case MoneyType.chipDeskMoney:
                return chipDeskMoneyAmount;
            case MoneyType.barMoney:
                return barMoneyAmount;
            default:
                return 0;
        }
    }
}

public enum MoneyType
{
    receptionMoney, 
    rouletteMoney, 
    baccaratMoney,
    jackpotMoney, 
    chipDeskMoney, 
    barMoney    
}


