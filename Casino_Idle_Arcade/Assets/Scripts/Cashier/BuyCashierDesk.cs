using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCashierDesk : MonoBehaviour
{
    [SerializeField] int cashierListIndex;
    [SerializeField] BuyAreaController controller;
    [SerializeField] CashierManager cashierManager;

    private void Update()
    {
        if (controller.price <= 0)
        {
            CustomerSpawner.instance.cashierManager[cashierListIndex] = cashierManager;
            Destroy(gameObject);
        }
    }
}
