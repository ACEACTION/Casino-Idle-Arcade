using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_ResetData : MonoBehaviour
{
    [SerializeField] Button resetBtn;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<HandStackData> handStackUpgradeDatas;
    [SerializeField] List<WorkerData> workerDatas;

    void Start()
    {
        resetBtn.onClick.AddListener(ResetProcess);
    }

    void ResetProcess()
    {
        GameManager.DeleteTotalMoneyFile();
        SaveLoadController.Instance.DeleteTutorialStateFile();
        SaveLoad_Settings.Instance.DeleteSettingDataFile();
        SaveLoad_CasinoElement.Instance.DeleteCasinoElementDataFile();
        PriorityController.Instance.DeleteOpenedPriorityFile();
        SaveLoad_Cashier.Instance.DeleteCashierData();
        SaveLoad_Workers.Instance.DeleteWorkersDataFile();

        LevelUpSlider.Instance.data.ResetData();

        ResetUpgradeData();

        SceneManager.LoadScene(0);
    }

    void ResetUpgradeData()
    {
        playerData.ResetData();

        for (int i = 0; i < handStackUpgradeDatas.Count; i++)
        {
            handStackUpgradeDatas[i].ResetData();
        }

        for (int i = 0; i < workerDatas.Count; i++)
        {
            workerDatas[i].ResetData();
        }

    }

}
