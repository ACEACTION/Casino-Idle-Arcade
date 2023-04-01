using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class CasinoElementManager
{

    public static List<CasinoElement> roulettes = new List<CasinoElement>();

    public static List<CasinoElement> jackPots = new List<CasinoElement>();


    public static List<CasinoElement> FindListByCasinoType(CustomerMovement customer)
    {
        switch (customer.elementType)
        {
            case ElementsType.roulette:
                return roulettes;
            case ElementsType.jackpot:
                return jackPots;
        }
        return null;
    }

    public static  bool SendCustomerToElement(CustomerMovement customer)
    {
       List<CasinoElement> elements =  FindListByCasinoType(customer);
        foreach (CasinoElement element in elements)
        {
            if (element.HasCapacity())
            {
                element.SendCustomerToElement(customer);
                return true;
            }

        }
        return false;
    }
}
