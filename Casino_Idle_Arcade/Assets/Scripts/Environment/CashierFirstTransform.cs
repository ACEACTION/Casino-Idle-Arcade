using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CashierFirstTransform : MonoBehaviour
{

    public CustomerMovement firstCustomer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            firstCustomer = other.gameObject.GetComponent<CustomerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            firstCustomer = null;

        }
    }
}
