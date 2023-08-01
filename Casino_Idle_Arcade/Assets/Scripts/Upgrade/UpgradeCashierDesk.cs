using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCashierDesk : MonoBehaviour
{
    public BuyAreaController bAController;
    [SerializeField] GameObject[] destroyedArea;
    bool notSaved;
    private void Update()
    {
        if (bAController.price <= 0 && !notSaved)
        {            
            notSaved = true;
            //SaveLoad_Cashier.Instance.SaveReception(this);
            SaveLoad_Cashier.Instance.SaveReception(this);

            DestroyAreas();   
            Destroy(gameObject);

        }
    }

    public void DestroyAreas()
    {
        for (int i = 0; i < destroyedArea.Length; i++)
        {
            Destroy(destroyedArea[i]);
        }
    }

}
