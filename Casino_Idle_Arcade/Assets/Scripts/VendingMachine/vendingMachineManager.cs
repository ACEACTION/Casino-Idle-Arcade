using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class vendingMachineManager 
{
    public static List<VendingMachine>  vendingMachines = new List<VendingMachine>();




    public static VendingMachine FindVendingMachine()
    {
        
            foreach (VendingMachine element in vendingMachines)
            {
                if (element.HasCapacity())
                    return element;
            }
            return null;
        
    }
    public static VendingMachine CanSendCustomerToVendingMachine()
    {
        return FindVendingMachine();
    }

    public static void ResetData()
    {
        vendingMachines.Clear();
    }
}
