using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCashierDesk : MonoBehaviour
{
    [SerializeField] BuyAreaController bAController;
    [SerializeField] GameObject[] destroyedArea;

    private void Update()
    {
        if (bAController.price <= 0)
        {            
            for (int i = 0; i < destroyedArea.Length; i++)
            {
                Destroy(destroyedArea[i]);
            }
            Destroy(gameObject);

        }
    }
}
