using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    int upgradeCost = 0;

    [SerializeField] Color disableItemBtnColor;
    [SerializeField] Color defaultItemBtnColor;
        
    [Header("Player Hand Stack")]
    [SerializeField] HandStackData playerStackData;
    [SerializeField] Upgrade_Item playerStackItem;

    [Header("Player Move Speed")]
    [SerializeField] PlayerData playerData;
    [SerializeField] Upgrade_Item playerMsItem;
    
    [Header("Worker Hand Stack")]
    [SerializeField] HandStackData workerStackData;


    [Header("Worker Panel")]
    [SerializeField] Image workerTitleBg;
    [SerializeField] GameObject workerPanel;


    private void Start()
    {
        defaultItemBtnColor = playerStackItem.item_bg.color;
        InitPlayerStack();
        InitPlayerMoveSpeed();
        InitWorkerPanel();
    }

    void InitPlayerStack()
    {
        InitPanel(playerStackData, playerStackItem);
    }

    void InitPlayerMoveSpeed()
    {
        InitPanel(playerData, playerMsItem);
    }

    void InitWorkerPanel()
    {
        workerPanel.SetActive(false);
        SetDisableBtnColor(workerTitleBg);
    }

    void InitPanel<T>(UpgradeData<T> upgradeData , Upgrade_Item item)
    {

        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            item.SetItemCost(upgradeCost);
        }
        else
        {
            item.SetDisableItemBtnColor(disableItemBtnColor);
            item.SetMaxLevelItem();
        }
    }

    public void UpgradePlayerHandStack()
    {
        if (playerStackData.CanUpgrade())
        {            
            upgradeCost = playerStackData.GetUpgradeCost();
            if (upgradeCost <= GameManager.totalMoney)
            {
                playerStackData.SetUgradeValue();
                GameManager.MinusMoney(upgradeCost);

                CanNextUpgrade(playerStackData, playerStackItem);
            }
        }
    }

    public void UpgradePlayerMoveSpeed()
    {
        if (playerData.CanUpgrade())
        {
            upgradeCost = playerData.GetUpgradeCost();
            if (upgradeCost <= GameManager.totalMoney)
            {
                playerData.SetUgradeValue();
                GameManager.MinusMoney(upgradeCost);

                CanNextUpgrade(playerData, playerMsItem);
            }
        }
    }



    void CanNextUpgrade<T>(UpgradeData<T> upgradeData, Upgrade_Item item)
    {
        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            item.SetItemCost(upgradeCost);
            if (upgradeCost > GameManager.totalMoney)
            {
                item.SetDisableItemBtnColor(disableItemBtnColor);
            }
        }
        else
        {
            item.SetDisableItemBtnColor(disableItemBtnColor);
            item.SetMaxLevelItem();
        }
    }

    
    public void SetDisableBtnColor(Image img) => img.color = disableItemBtnColor;
    
    public void SetEnableBtnColor(Image img) => img.color = defaultItemBtnColor;
}
