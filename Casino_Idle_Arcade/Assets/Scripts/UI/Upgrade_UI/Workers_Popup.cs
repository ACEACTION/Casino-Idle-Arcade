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

    [Header("Cleaner 1")]
    [SerializeField] WorkerData cleanerData1;
    [SerializeField] Upgrade_Item cleanerItem1;

    [Header("Cleaner 2")]
    [SerializeField] WorkerData cleanerData2;
    [SerializeField] Upgrade_Item cleanerItem2;

    [Header("Chip Deliver 1")]
    [SerializeField] HandStackData chipDeliverStackData1;
    [SerializeField] Upgrade_Item chipDeliverStackItem1;
    [SerializeField] WorkerData chipDeliverData1;
    [SerializeField] Upgrade_Item chipDeliverSpeedItem1;


    [Header("Chip Deliver 2")]
    [SerializeField] HandStackData chipDeliverStackData2;
    [SerializeField] Upgrade_Item chipDeliverStackItem2;
    [SerializeField] WorkerData chipDeliverData2;
    [SerializeField] Upgrade_Item chipDeliverSpeedItem2;



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
        UpgradeProcess(playerStackItem, playerStack);
    }

    public void PlayerSpeedBtn()
    {
        UpgradeProcess(playerSpeedItem, playerData);
    }

    public void CleanerSpeed1Btn()
    {
        UpgradeProcess(cleanerItem1, cleanerData1);
    }

    public void CleanerSpeed2Btn()
    {
        UpgradeProcess(cleanerItem2, cleanerData2);
    }

    public void ChipDeliveHandStack1Btn()
    {
        UpgradeProcess(chipDeliverStackItem1, chipDeliverStackData1);
    }

    public void ChipDeliveSpeed1Btn()
    {
        UpgradeProcess(chipDeliverSpeedItem1, chipDeliverData1);
    }



    public void ChipDeliveHandStack2Btn()
    {
        UpgradeProcess(chipDeliverStackItem2, chipDeliverStackData2);
    }

    public void ChipDeliveSpeed2Btn()
    {
        UpgradeProcess(chipDeliverSpeedItem2, chipDeliverData2);
    }


    void UpgradeProcess<T>(Upgrade_Item item, UpgradeData<T> data)
    {
        item.UpgradeItem(data);
    }

}   
