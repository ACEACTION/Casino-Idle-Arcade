using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAreaController : MonoBehaviour
{
    [SerializeField] BuyAreaController bAController;
    [SerializeField] CasinoElement casinoElement;

    private void Update()
    {
        if (bAController.price <= 0)
        {
            casinoElement.UpgradeElements();
            SaveLoad_CasinoElement.Instance.AddItemToElementsSaveDatas(casinoElement);

            Destroy(gameObject);
        }
    }


}
