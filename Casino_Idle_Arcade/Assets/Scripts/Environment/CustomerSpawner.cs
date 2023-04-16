using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{


    public CashierManager[] cashierManager;

    public int counter = 0;
    public CustomerMovement cs;
    public static CustomerSpawner instance;
    public int maxCustomer;

    public bool cmChanger;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] GameObject Customer; 
    [SerializeField] float spawnTime = 1f; 
    public Transform spawnPoint; 
    void Start()
    {
        if (GameManager.isCompleteTutorial)
            SpawnCustomers();
    }

    public void SpawnCustomers()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnTime); 
    }


    void SpawnObject()
    {
        if (counter < maxCustomer)
        {
            //Getting from CustomerPool

            if (cashierManager[0] != null && cmChanger &&  cashierManager[0].customersList.Count < cashierManager[0].maxCapacity)
            {
                cs = CustomerPool.instance.customerPool.Get();
                cs.isLeaving = false;
                cashierManager[0].customersList.Add(cs);
                cs.SetMove(cashierManager[0].customerSpots[cashierManager[0].customersList.IndexOf(cs)]);
                counter++;

            }

            if (cashierManager[1] != null && !cmChanger && cashierManager[1].customersList.Count < cashierManager[1].maxCapacity)
            {
                cs = CustomerPool.instance.customerPool.Get();
                cs.isLeaving = false;
                cashierManager[1].customersList.Add(cs);
                cs.SetMove(cashierManager[1].customerSpots[cashierManager[1].customersList.IndexOf(cs)]);
                counter++;


            }
            cs.transform.position = spawnPoint.position;
            cmChanger = !cmChanger;

        }

    }


    public  void AllCustomerInLineMoveForward(CashierManager cashiermanager)
    {
        for (int i = 0; i < cashiermanager.customersList.Count; i++)
        {
            cashiermanager.customersList[i].SetMove(cashiermanager.customerSpots[i]);  
        }
    }

}
