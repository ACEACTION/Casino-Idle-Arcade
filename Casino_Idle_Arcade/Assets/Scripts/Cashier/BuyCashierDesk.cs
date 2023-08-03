using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCashierDesk : MonoBehaviour
{
    [SerializeField] int cashierListIndex;
    public BuyAreaController controller;
    [SerializeField] CashierManager cashierManager;

    bool notSaved;
    private void Update()
    {
        if (controller.price <= 0 && !notSaved)
        {
            notSaved = true;
            SaveLoad_Cashier.Instance.SaveCashierDesk(controller);
            CustomerSpawner.instance.cashierManager[cashierListIndex] = cashierManager;
            Destroy(gameObject);
        }
    }
}
