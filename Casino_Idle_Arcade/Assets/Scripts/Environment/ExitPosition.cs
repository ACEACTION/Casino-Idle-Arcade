using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPosition : MonoBehaviour
{
    public static ExitPosition instance;
    public Transform customerSpot;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            other.gameObject.GetComponent<CustomerMovement>().ReleaseCustomer();
        }
    }
}
