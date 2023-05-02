using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public CasinoResourceDesk casinoResource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Casino Resource"))
        {
            casinoResource = other.GetComponent<CasinoResourceDesk>();
        }

     

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Casino Resource"))
        {
            casinoResource = null;
        }

      

    }
}
