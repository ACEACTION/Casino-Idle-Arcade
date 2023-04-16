using System;
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
    public CasinoElementSpotController spotController;
    
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

    public virtual void CustomerHasArrived()
    {
        
    }

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
        foreach (CasinoElementSpot spot in spotController.elementSpots)
        {
            if (spot.IsEmptySpot()) return spot;
        }
        return null;
    }

    public void SeSpotController(CasinoElementSpotController controller)
    {
        spotController = controller;
    }

    public void SetNullElementSpotsCustomer()
    {
        foreach (CasinoElementSpot spots in spotController.elementSpots)
        {
            spots.customer = null;
        }
    }



    public virtual void UpgradeElements()
    {        
        StartCoroutine(UpgradeProcess());
    }

    IEnumerator UpgradeProcess()
    {
        SetNullElementSpotsCustomer();
        upgradeModels[upgradeIndex].SetActive(false);
        upgradeIndex++;
        upgradeModels[upgradeIndex].SetActive(true);
        maxGameCapacity = upgradeCapacity[upgradeIndex];

        yield return new WaitForSeconds(.7f);

        //spotController.ResetElementSpot();        

        yield return new WaitForSeconds(.5f);
        foreach (CustomerMovement cm in customers)
        {
            SendCustomerToSpot(cm);
        }

        customerCounter = 0;

        //foreach (CasinoElementSpot spot in spotController.elementSpots)
        //{
        //    spot.gameObject.SetActive(false);
        //    spot.gameObject.SetActive(true);
        //}
        
        spotController.ResetElementSpot();        


    }

}


