using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Cashier Data")]
public class CashierData : ScriptableObject
{
    [SerializeField] int jackPotPayment;
    [SerializeField] int roulettePayment;       
    public float cooldownAmount;

    [SerializeField] Sprite[] icons;


    public Sprite GetIcon(ElementsType type) => icons[(int)type];

    public int GetPayment(ElementsType type)
    {
        switch (type)
        {
            case ElementsType.roulette:
                return roulettePayment;
            case ElementsType.jackpot:
                return jackPotPayment;
            default:
                return 1;
        }
    }

}

public class CasinoGameIcon
{
    public Sprite icon;
    public ElementsType type;
}
