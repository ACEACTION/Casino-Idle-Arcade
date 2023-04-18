using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class CasinoElementManager
{

    public static List<CasinoElement> roulettes = new List<CasinoElement>();

    public static List<CasinoElement> jackPots = new List<CasinoElement>();

    //static List<CasinoElement> elements = new List<CasinoElement>();
    static CasinoElement casinoElement;
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
  
    public static CasinoElement CanSendCustomerToElement(CustomerMovement customer)
    {
        List<CasinoElement> elements = FindListByCasinoType(customer);
        foreach (CasinoElement element in elements)
        {
            if (element.HasCapacity())
            {                
                return element;
            }
        }
        return null;
    }  

}
