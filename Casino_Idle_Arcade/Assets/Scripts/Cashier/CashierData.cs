using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Cashier Data")]
public class CashierData : ScriptableObject
{
    [SerializeField] int jackPotPayment;
    [SerializeField] int roulettePayment;       
    public float cooldownAmount;

    [SerializeField] Sprite rouletteIcon;
    [SerializeField] Sprite jackpotIcon;
    [SerializeField] Sprite barIcon;



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


    public Sprite GetCasinoGameIcon(ElementsType type)
    {
        switch (type) 
        {
            case ElementsType.roulette:
                return rouletteIcon;
            case ElementsType.jackpot:
                return jackpotIcon;
            case ElementsType.bar:
                return barIcon;
            default:
                return null;
        }

    }

}

public class CasinoGameIcon
{
    public Sprite icon;
    public ElementsType type;
}
