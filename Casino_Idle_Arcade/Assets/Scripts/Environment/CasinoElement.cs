using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoElement : MonoBehaviour
{
    public bool isCapacityFull;
    public int upgradeIndex;
    public int maxGameCapacity;
    public int customerCounter;
    [SerializeField] int[] upgradeCapacity;
    [SerializeField] GameObject[] upgradeModels;
    public List<CustomerMovement> customers = new List<CustomerMovement>();
    public List<CasinoElementSpot> spotList;
    public List<CasinoElementSpotSlot> elementSpotSlots = new List<CasinoElementSpotSlot>();

    public bool HasCapacity() =>  customers.Count < maxGameCapacity;        


    public void AddToCustomerList(CustomerMovement customer) => customers.Add(customer);

    public void RemoveFromCustomerList(CustomerMovement customer) => customers.Remove(customer);

    private void Start()
    {

        // **********Load Data**************
        
        // load upgradeIndex
        //if (upgradeIndex < upgradeModels.Length - 1)
        //{
        //    upgradePriority.ActiveUpgradeController(upgradeIndex);
        //}
    }

    public virtual void CustomerHasArrived() { }

    public virtual void CustomerLeft() { }

    public void SendCustomerToElement(CustomerMovement customer)
    {
        SendCustomerToSpot(customer);
        customers.Add(customer);
    }

    void SendCustomerToSpot(CustomerMovement customer)
    {
        CasinoElementSpot spot = GetEmptySpot();
        if (spot != null)
        {
            customer.SetMove(spot.transform);
            spot.customer = customer;
        }
    }

    public CasinoElementSpot GetEmptySpot()
    {
        foreach (CasinoElementSpot spot in spotList)
        {
            if (spot.IsEmptySpot()) return spot;
        }
        return null;
    }

    public void SetNullElementSpotsCustomer()
    {
        foreach (CasinoElementSpot spot in spotList)
        {
            spot.customer = null;
        }
    }


    

    public virtual void UpgradeElements()
    {                
        upgradeModels[upgradeIndex].SetActive(false);
        upgradeIndex++;
        upgradeModels[upgradeIndex].SetActive(true);
        maxGameCapacity = upgradeCapacity[upgradeIndex];

        foreach (CasinoElementSpot spot in elementSpotSlots[upgradeIndex].elementSpots)
        {
            spot.gameObject.SetActive(true);
            spotList.Add(spot);
        }

    }

    
}

[System.Serializable]
public class CasinoElementSpotSlot
{
    public List<CasinoElementSpot> elementSpots = new List<CasinoElementSpot>();
}
