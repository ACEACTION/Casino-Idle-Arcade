using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class CasinoElementManager
{

    public static List<CasinoElement> roulettes = new List<CasinoElement>();

    public static List<CasinoElement> jackPots = new List<CasinoElement>();

    public static List<CasinoElement> allCasinoElements = new List<CasinoElement>();

    //static List<CasinoElement> elements = new List<CasinoElement>();
    public static CasinoElement element;
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
  
    public static CasinoElement FindCasinoElement()
    {
        foreach (CasinoElement element in allCasinoElements)
        {
            if (element.HasCapacity())
                return element;
        }
        return null;
    }

    public static CasinoElement CanSendCustomerToElement()
    {
        return FindCasinoElement();
    }  

}
