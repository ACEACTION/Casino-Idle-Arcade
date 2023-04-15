using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CashierFirstTransform : MonoBehaviour
{

    public bool nextCustomer;
    public CustomerMovement firstCustomer;
    [SerializeField] CashierManager cashierManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            
            if (!other.gameObject.GetComponent<CustomerMovement>().isLeaving)
            {
                firstCustomer = other.gameObject.GetComponent<CustomerMovement>();
                firstCustomer.fCustomer = true;
                nextCustomer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (firstCustomer != null)
        {
            if (other.gameObject.Equals(firstCustomer.gameObject))
            {
                cashierManager.customersList.Remove(firstCustomer);
                CustomerSpawner.instance.AllCustomerInLineMoveForward(cashierManager);
                CustomerSpawner.instance.counter--;
                firstCustomer = null;

            }

;
        }

    }
}
