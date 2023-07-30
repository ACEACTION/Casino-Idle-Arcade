using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBtn : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] Button closeBtn;

    void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            settingPanel.SetActive(true);
            AudioSourceManager.Instance.PlayPopup();
        });
    }

}
