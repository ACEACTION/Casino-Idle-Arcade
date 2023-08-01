using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{

    [SerializeField] float openWinDuration;
    [SerializeField] Ease openWindowEase;
    [SerializeField] Ease closeWindowEase;
    [SerializeField] Transform settingWindow;
    [SerializeField] Button closeBtn;

    private void Awake()
    {
        settingWindow.transform.localScale = Vector3.zero;        
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(CloseWindow);
        
        CloseWindow();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OpenWindow();
    }

    void OpenWindow()
    {
        settingWindow.DOScale(1, openWinDuration).SetEase(openWindowEase);
    }

    void CloseWindow()
    {
        SaveLoad_Settings.Instance.SaveSettings(GameManager.sfx, GameManager.music);
        AudioSourceManager.Instance.PlayPopup();
        settingWindow.DOScale(0, openWinDuration).SetEase(closeWindowEase).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
