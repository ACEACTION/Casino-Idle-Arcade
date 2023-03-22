using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoElementSpot : MonoBehaviour
{
    [SerializeField] CasinoElement gi;
    public CustomerMovement customer;



    public bool IsEmptySpot() => customer == null;

    private void OnTriggerEnter(Collider other)
    {
        if (customer != null)
        {

            if (other.gameObject.Equals(customer.gameObject))
            {
                gi.CustomerHasArrived();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //
    }

    
}
