using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_ResetData : MonoBehaviour
{
    [SerializeField] Button resetBtn;

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

        SceneManager.LoadScene(0);
    }
}
