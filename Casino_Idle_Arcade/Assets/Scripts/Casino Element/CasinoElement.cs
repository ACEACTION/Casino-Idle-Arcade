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
    public GameObject buyAreaController;
    [SerializeField] int[] upgradeCapacity;
    [SerializeField] GameObject[] upgradeModels;
    public List<CustomerMovement> customers = new List<CustomerMovement>();
    public List<CasinoElementSpot> spotList;
    public List<CasinoElementSpotSlot> elementSpotSlots = new List<CasinoElementSpotSlot>();
    public virtual void Start()
    {
        InitElementProcess();  
    }

    public virtual void InitElementProcess()
    {
        // load model process
        for (int i = 0; i < upgradeModels.Length; i++)
        {
            upgradeModels[i].SetActive(false);
        }
        GetModel().gameObject.SetActive(true);
        
        //if (buyAreaController)
        //    buyAreaController.SetActive(false);

        // customer capacity process
        maxGameCapacity = upgradeCapacity[upgradeIndex];

        // spot list process
        spotList.Clear();
        for (int i = 0; i <= upgradeIndex; i++)
        {
            spotList.AddRange(elementSpotSlots[i].elementSpots);
        }
    }


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

    public virtual StackMoney GetStackMoney() => null;

}

[System.Serializable]
public class CasinoElementSpotSlot
{
    public List<CasinoElementSpot> elementSpots = new List<CasinoElementSpot>();
}
