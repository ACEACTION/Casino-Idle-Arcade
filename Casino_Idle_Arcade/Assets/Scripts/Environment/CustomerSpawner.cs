using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    public  CashierManager cashierManager;
    public  List<CustomerMovement> customersList = new List<CustomerMovement>();

    public static CustomerSpawner instance;
    public int maxCustomer;
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
        InvokeRepeating("SpawnObject", spawnTime, spawnTime); 
    }

    void SpawnObject()
    {
        if (customersList.Count < maxCustomer)
        {
            //Getting from CustomerPool
            var cs = CustomerPool.instance.customerPool.Get();
            customersList.Add(cs);
            cs.transform.position = spawnPoint.position;
            var i = customersList.IndexOf(cs);
            cs.SetMove(cashierManager.customerSpots[i]);
        }
        
    }
    public  void AllCustomerInLineMoveForward()
    {
        for (int i = 0; i < customersList.Count; i++)
        {
            customersList[i].SetMove(cashierManager.customerSpots[i]);
        }
    }

}
