using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_Item : MonoBehaviour
{
    // references        
    [SerializeField] string lvlContent;
    [SerializeField] Upgrade_Item_Btn item_btn;           

    public void InitItemBtn<T>(UpgradeData<T> upgradeData)
    {

        if (upgradeData.CanUpgrade())
        {
            item_btn.InitItemTxt(string.Concat(upgradeData.farsiUpgradeName, " ", lvlContent, " ", upgradeData.upgradeLevelCounter + 1)
                    , upgradeData.GetUpgradeCost());
        }

        item_btn.SetMaxedState(upgradeData.CanUpgrade());
    }

    public void UpgradeItem<T>(UpgradeData<T> upgradeData, WorkerUpgradeEffect upgradeEff)
    {        
        if (upgradeData.CanUpgrade())
        {
            if (GameManager.GetTotalMoney() >= upgradeData.GetUpgradeCost())
            {
                GameManager.MinusMoney(upgradeData.GetUpgradeCost());
                Money_UI.Instance.SetTotalMoneyTxt();
                upgradeData.SetUgradeValue();
                InitItemBtn(upgradeData);
                AudioSourceManager.Instance.PlayBuyItem();
                
                CameraFollow.instance.SetDynamicFollow_UpgradeWorker(upgradeEff.transform.parent
                    , upgradeEff, upgradeData.farsiUpgradeName);


                LevelUpSlider.Instance.SetLevelUp(LevelUpSlider.Instance.starIcon.position);
            }
            else
            {
                AudioSourceManager.Instance.PlayCantBuyItem();
                Money_UI.Instance.ShakeMoneyUI();
            }
        }
        else
            AudioSourceManager.Instance.PlayCantBuyItem();
    }    

}
