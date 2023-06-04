using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Workers_Popup : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] PlayerHandStackData playerStack;
    [SerializeField] Upgrade_Item playerStackItem;
    [SerializeField] PlayerData playerData;
    [SerializeField] Upgrade_Item playerSpeedItem;
    [SerializeField] WorkerUpgradeEffect playerEff;


    [Header("Cleaner 1")]
    [SerializeField] WorkerData cleanerData1;
    [SerializeField] Upgrade_Item cleanerItem1;
    [SerializeField] WorkerUpgradeEffect cleaner1Eff;


    [Header("Cleaner 2")]
    [SerializeField] WorkerData cleanerData2;
    [SerializeField] Upgrade_Item cleanerItem2;    
    [SerializeField] WorkerUpgradeEffect cleaner2Eff;


    [Header("Chip Deliver 1")]
    [SerializeField] HandStackData chipDeliverStackData1;
    [SerializeField] Upgrade_Item chipDeliverStackItem1;
    [SerializeField] WorkerData chipDeliverData1;
    [SerializeField] Upgrade_Item chipDeliverSpeedItem1;    
    [SerializeField] WorkerUpgradeEffect chipDeliver1Eff;


    [Header("Chip Deliver 2")]
    [SerializeField] HandStackData chipDeliverStackData2;
    [SerializeField] Upgrade_Item chipDeliverStackItem2;
    [SerializeField] WorkerData chipDeliverData2;
    [SerializeField] Upgrade_Item chipDeliverSpeedItem2;
    [SerializeField] WorkerUpgradeEffect chipDeliver2Eff;



    private void Start()
    {
        playerStackItem.InitItemBtn(playerStack);
        playerSpeedItem.InitItemBtn(playerData);
        cleanerItem1.InitItemBtn(cleanerData1);
        cleanerItem2.InitItemBtn(cleanerData2);
        chipDeliverStackItem1.InitItemBtn(chipDeliverStackData1);
        chipDeliverSpeedItem1.InitItemBtn(chipDeliverData1);
        chipDeliverStackItem2.InitItemBtn(chipDeliverStackData2);
        chipDeliverSpeedItem2.InitItemBtn(chipDeliverData2);
    }


    public void PlayerHandStackBtn()
    {
        UpgradeProcess(playerStackItem, playerStack, playerEff);
    }

    public void PlayerSpeedBtn()
    {
        UpgradeProcess(playerSpeedItem, playerData, playerEff);
    }

    public void CleanerSpeed1Btn()
    {
        UpgradeProcess(cleanerItem1, cleanerData1, cleaner1Eff);
    }

    public void CleanerSpeed2Btn()
    {
        UpgradeProcess(cleanerItem2, cleanerData2, cleaner2Eff);
    }

    public void ChipDeliveHandStack1Btn()
    {
        UpgradeProcess(chipDeliverStackItem1, chipDeliverStackData1, chipDeliver1Eff);
    }

    public void ChipDeliveSpeed1Btn()
    {
        UpgradeProcess(chipDeliverSpeedItem1, chipDeliverData1, chipDeliver1Eff);
    }



    public void ChipDeliveHandStack2Btn()
    {
        UpgradeProcess(chipDeliverStackItem2, chipDeliverStackData2, chipDeliver2Eff);
    }

    public void ChipDeliveSpeed2Btn()
    {
        UpgradeProcess(chipDeliverSpeedItem2, chipDeliverData2, chipDeliver2Eff);
    }


    void UpgradeProcess<T>(Upgrade_Item item, UpgradeData<T> data, WorkerUpgradeEffect upgradeEff)
    {
        item.UpgradeItem(data, upgradeEff);
    }

}   
