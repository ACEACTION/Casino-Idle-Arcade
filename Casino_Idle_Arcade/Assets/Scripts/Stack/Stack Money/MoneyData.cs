using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MoneyData")]
public class MoneyData : CasinoResourceData
{
    public int moneyPrice;
    public float moneyGoToCustomerFromDeskTime;
    public float goToPlayerTime;
}
