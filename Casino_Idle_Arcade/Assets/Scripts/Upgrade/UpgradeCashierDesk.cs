using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCashierDesk : MonoBehaviour
{
    [SerializeField] BuyAreaController bAController;
    [SerializeField] GameObject upgradeEffect;
    [SerializeField] GameObject receptioner;

    private void Update()
    {
        if (bAController.price <= 0)
        {
            upgradeEffect.SetActive(true);
            receptioner.SetActive(true);
            Destroy(gameObject);
        }
    }
}
