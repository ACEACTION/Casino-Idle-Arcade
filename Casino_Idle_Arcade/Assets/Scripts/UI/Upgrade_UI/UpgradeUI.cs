using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    int upgradeCost = 0;
    [SerializeField] Color disableItemBtnColor;
    [SerializeField] Color defaultItemBtnColor;
    float btnPressedScale;
    [SerializeField] TextMeshProUGUI statusTxt;
    Vector3 statusOriginPos;

    [Header("Player Hand Stack")]
    [SerializeField] PlayerHandStackData playerStackData;
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
    [SerializeField] GameObject workerTitleLockIcon;
    [SerializeField] GameObject workerStackLockIcon;

    public static UpgradeUI Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    private void Start()
    {
        defaultItemBtnColor = playerStackItem.item_bg.color;
        btnPressedScale = playerStackItem.item_btn.transform.localScale.x;
        statusOriginPos = statusTxt.transform.position;

    }

    public void OpenMainPanel()
    {
        OpenPlayerPanel();
    }


    public void OpenPlayerPanel()
    {
        InitPlayerPanel();

        SetPanelState(true, playerPanel, playerTitleBg);
        SetPanelState(false, workerPanel, workerTitleBg);

        SetBtnScale(playerTitleBg.transform);

    }

    public void OpenWorkerPanel()
    {
        if (WorkerManager.BuyedWorker())
        {

            InitWorkerPanel();

            SetPanelState(false, playerPanel, playerTitleBg);
            SetPanelState(true, workerPanel, workerTitleBg);

        }
        else
            ShowStatusTxt("Worker is not open!");
        
        SetBtnScale(workerTitleBg.transform);
    }

    void InitPlayerPanel()
    {
        InitPanel(playerStackData, playerStackItem);
        InitPanel(playerData, playerMsItem);
    }

    void InitWorkerPanel()
    {
        InitPanel(workerStackData, workerStaclItem);
        InitPanel(workerMoveSpeedData, workerMsItem);
    }

    void SetPanelState(bool state, GameObject panel, Image bg)
    {               
        panel.SetActive(state);
        SetBtnColorState(state, bg);
        
        // play title panel sfx
        AudioSourceManager.Instance.PlaySwitchTabIcon();
    }

 
    void InitPanel<T>(UpgradeData<T> upgradeData , Upgrade_Item item)
    {

        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            item.SetItemCost(upgradeCost);
            if (GameManager.GetTotalMoney() >= upgradeCost )
                item.SetBtnBgColor(defaultItemBtnColor);
            else
                item.SetBtnBgColor(disableItemBtnColor);
        }
        else
        {
            item.SetBtnBgColor(disableItemBtnColor);
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
        if (WorkerManager.chipDeliverIsOpened)
            UpgradeProcess(workerStackData, workerStaclItem);
        else
        {
            SetBtnScale(workerStaclItem.transform);
            ShowStatusTxt("Worker is not open!");
        }
    }


    public void UpgradeWorkerMoveSpeed()
    {

        UpgradeProcess(workerMoveSpeedData, workerMsItem);
        WorkerManager.SetAgentMoveSpeed();
    }

    public void UpgradeProcess<T>(UpgradeData<T> upgradeData, Upgrade_Item item)
    {
        SetBtnScale(item.item_btn.transform);
        // play upgrade sfx
        AudioSourceManager.Instance.PlayBuyItem();

        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            if (upgradeCost <= GameManager.GetTotalMoney())
            {
                upgradeData.SetUgradeValue();

                GameManager.MinusMoney(upgradeCost);
                Money_UI.Instance.SetTotalMoneyTxt();

                CanNextUpgrade(upgradeData, item);


                InitPlayerPanel();
                InitWorkerPanel();
            }
            else
                ShowStatusTxt("Money is not enough!");
        }
    }


    void CanNextUpgrade<T>(UpgradeData<T> upgradeData, Upgrade_Item item)
    {
        if (upgradeData.CanUpgrade())
        {
            upgradeCost = upgradeData.GetUpgradeCost();
            item.SetItemCost(upgradeCost);
            if (upgradeCost > GameManager.GetTotalMoney())
            {
                item.SetBtnBgColor(disableItemBtnColor);
            }
        }
        else
        {
            item.SetBtnBgColor(disableItemBtnColor);
            item.SetMaxLevelItem();
        }
    }

    void ShowStatusTxt(string msg)
    {
        statusTxt.transform.DOKill();
        statusTxt.text = msg;
        statusTxt.gameObject.SetActive(true);
        statusTxt.transform.position = statusOriginPos;
        statusTxt.transform.DOMoveY(statusOriginPos.y + 280, 1.5f).OnComplete(() =>
        {
            statusTxt.gameObject.SetActive(false);
        });

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


    public void UnlockWorkerTitle()
    {
        workerTitleLockIcon.SetActive(false);
    }

    public void UnlockWorkerStackItem()
    {
        workerStackLockIcon.SetActive(false);
        workerStaclItem.SetBtnBgColor(defaultItemBtnColor);
    }


    

}
