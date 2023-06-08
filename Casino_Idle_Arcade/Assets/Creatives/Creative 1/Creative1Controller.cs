using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creative1Controller : MonoBehaviour
{
    public List<GameObject> jackPots = new List<GameObject>();
    public CashierManager cm2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //time to spawn jackpots
            foreach(GameObject x in jackPots)
            {
                x.SetActive(true);
            }
            CustomerSpawner.instance.cashierManager[1] = cm2;
            cm2.gameObject.SetActive(true);
            print("keydown");


        }
    }
}
