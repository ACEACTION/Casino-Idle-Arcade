using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrade_Icon_Btn : MonoBehaviour
{
    [SerializeField] float openUpgradePanelDuration;
    [SerializeField] Ease openEase;
    [SerializeField] Ease closeEase;
    [SerializeField] GameObject joystick;
    bool wasSelected = false;
    [SerializeField] Button btn;
    [SerializeField] MainUpgradePanel upgradeMainPanel;

    Vector3 upgradePanelDefaultScale;

    void Start()
    {
        upgradePanelDefaultScale = upgradeMainPanel.mainWindow.localScale;
        upgradeMainPanel.mainWindow.localScale = Vector3.zero;
        btn.onClick.AddListener(OpenUpgradeMainPanel);
        upgradeMainPanel.closeBtn.onClick.AddListener(CloseUpgradeMainPanel);
    }

    void OpenUpgradeMainPanel()
    {
        gameObject.SetActive(false);
        upgradeMainPanel.gameObject.SetActive(true);
        upgradeMainPanel.mainWindow.DOScale(upgradePanelDefaultScale, openUpgradePanelDuration).SetEase(openEase);
        AudioSourceManager.Instance.OpenWindowUi();
        joystick.SetActive(false);
    }


    void CloseUpgradeMainPanel()
    {
        if (!wasSelected)
        {
            AudioSourceManager.Instance.CloseWindowUi();
            wasSelected = true;
            upgradeMainPanel.mainWindow.DOScale(0, openUpgradePanelDuration).SetEase(closeEase).OnComplete(() =>
            {
                gameObject.SetActive(true);
                upgradeMainPanel.gameObject.SetActive(false);
                wasSelected = false;
            });
        }
        joystick.SetActive(true);
    }

}
