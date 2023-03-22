using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoElement : MonoBehaviour
{
    public int castTime;
    public int castTimeAmount;
    public bool isCapacityFull;
    public List<CustomerMovement> customers = new List<CustomerMovement>();
    public List<CasinoElementSpot> elementSpot = new List<CasinoElementSpot>();
    public int maxGameCapacity;
    public int customerCounter;

    public bool HasCapacity() =>  customers.Count <= maxGameCapacity;

    public void AddToCustomerList(CustomerMovement customer) => customers.Add(customer);

    public void RemoveFromCustomerList(CustomerMovement customer) => customers.Remove(customer);


    public virtual void CustomerHasArrived()
    {
        
    }

    public CasinoElementSpot GetEmptySpot()
    {
        foreach (CasinoElementSpot spot in elementSpot)
        {
            if (spot.IsEmptySpot()) return spot;
        }

        return null;
    }

    public void SendCustomerToElement(CustomerMovement customer)
    {
        CasinoElementSpot spot = GetEmptySpot();
        customer.SetMove(spot.transform);
        spot.customer = customer;
        customers.Add(customer);
    }
}
