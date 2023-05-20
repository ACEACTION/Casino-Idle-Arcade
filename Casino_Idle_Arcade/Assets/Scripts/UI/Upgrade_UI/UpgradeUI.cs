using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeUI : MonoBehaviour
{
    int upgradeCost = 0;
    [SerializeField] Color disableItemBtnColor;
    [SerializeField] Color defaultItemBtnColor;
    float btnPressedScale; 

    [Header("Player Hand Stack")]
    [SerializeField] HandStackData playerStackData;
    [SerializeField] Upgrade_Item playerStackItem;

    [Header("Player Move Speed")]
    [SerializeField] PlayerData playerData;
    [SerializeField] Upgrade_Item playerMsItem;
    
    [Header("Worker Move Speed")]
    [SerializeField] WorkerData workerMoveSpeedData;
    [SerializeField] Upgrade_Item workerMsItem;

    [Header("Worker Hand Stack")]
    [SerializeField] HandStackData workerStackData;
    [SerializeField] Upgrade_Item workerStaclItem;

    [Header("Title & Panels")]
    [SerializeField] Image playerTitleBg;
    [SerializeField] GameObject playerPanel;
    [SerializeField] Image workerTitleBg;
    [SerializeField] GameObject workerPanel;



    private void Start()
    {
        defaultItemBtnColor = playerStackItem.item_bg.color;
        btnPressedScale = playerStackItem.item_btn.transform.localScale.x;
        OpenPlayerPanel();
    }

    public void OpenPlayerPanel()
    {
        InitPanel(playerStackData, playerStackItem);
        InitPanel(playerData, playerMsItem);        

        SetPanelState(true, playerPanel, playerTitleBg);
        SetPanelState(false, workerPanel, workerTitleBg);

        SetBtnScale(playerTitleBg.transform);

    }

    public void OpenWorkerPanel()
    {
        InitPanel(workerStackData, workerStaclItem);
        InitPanel(workerMoveSpeedData, workerMsItem);

        SetPanelState(false, playerPanel, playerTitleBg);
        SetPanelState(true, workerPanel, workerTitleBg);

        SetBtnScale(workerTitleBg.transform);
    }

    void SetPanelState(bool state, GameObject panel, Image bg)
    {               
        panel.SetActive(state);
        SetBtnColorState(state, bg);

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
        UpgradeProcess(playerStackData, playerStackItem);
    }

    public void UpgradePlayerMoveSpeed()
    {
        UpgradeProcess(playerData, playerMsItem);
    }

    public void UpgradeWorkerHandStack()
    {
        UpgradeProcess(workerStackData, workerStaclItem);
    }




    public void UpgradeWorkerMoveSpeed()
    {
        UpgradeProcess(workerMoveSpeedData, workerMsItem);
        WorkerManager.SetAgentMoveSpeed();
    }

    public void UpgradeProcess<T>(UpgradeData<T> upgradeData, Upgrade_Item item)
    {
        SetBtnScale(item.item_btn.transform);
        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            if (upgradeCost <= GameManager.totalMoney)
            {
                upgradeData.SetUgradeValue();
                GameManager.MinusMoney(upgradeCost);

                CanNextUpgrade(upgradeData, item);
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


    void SetBtnColorState(bool state, Image img) {
        if (state)
            img.color = defaultItemBtnColor; 
        else
            img.color = disableItemBtnColor;
    }

    void SetBtnScale(Transform btn)
    {
        btn.transform.DOScale(btnPressedScale + .1f, .4f).OnComplete(() =>
        {
            btn.transform.DOScale(btnPressedScale, .4f);
        });

    }

}
