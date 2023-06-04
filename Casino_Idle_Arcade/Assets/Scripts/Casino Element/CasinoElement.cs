using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CasinoElement : MonoBehaviour
{

    public ElementsType elementType;
    public int upgradeIndex;
    public int maxGameCapacity;
    public int customerCounter;
    [SerializeField] int[] upgradeCapacity;
    [SerializeField] GameObject[] upgradeModels;
    public List<CustomerMovement> customers = new List<CustomerMovement>();
    public List<CasinoElementSpot> spotList;
    public List<CasinoElementSpotSlot> elementSpotSlots = new List<CasinoElementSpotSlot>();


    public bool HasCapacity() => customers.Count < maxGameCapacity;


    public void AddToCustomerList(CustomerMovement customer) => customers.Add(customer);

    public void RemoveFromCustomerList(CustomerMovement customer) => customers.Remove(customer);

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


    public Transform GetModel() => upgradeModels[upgradeIndex].transform;
    public void ShakeModel() 
    {
        //  GetModel().DOShakeScale(1f, 0.5f); 
        var defaultScale = GetModel().localScale;
        GetModel().localScale = new Vector3(0.01f, 0.01f, 0.01f);
        GetModel().DOScale(defaultScale, 0.7f).SetEase(Ease.OutBounce);
    }
    
}

[System.Serializable]
public class CasinoElementSpotSlot
{
    public List<CasinoElementSpot> elementSpots = new List<CasinoElementSpot>();
}
