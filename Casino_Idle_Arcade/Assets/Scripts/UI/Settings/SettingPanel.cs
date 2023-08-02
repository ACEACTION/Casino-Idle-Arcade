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
    [SerializeField] Setting_Sfx setting_Sfx;
    [SerializeField] Setting_Music setting_Music;
    [SerializeField] Button closeBtn;
    bool wasSelected = false;

    public bool defaultGameMngrSfx;

    private void Awake()
    {
        settingWindow.transform.localScale = Vector3.zero;        
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(CloseWindow);

        /* we cant add playCloseSfx to CloseWindowMethod, because this gameobject active
             and deactive in begins of game and play sfx, so we added to button in seperate */
        closeBtn.onClick.AddListener(() =>
        {
            AudioSourceManager.Instance.CloseWindowUi();
        });

        // if GameManager.sfx is on, the sfx is listened in beginning of game (the setting menu is not open)
        defaultGameMngrSfx = GameManager.sfx;
        GameManager.sfx = false;
        setting_Sfx.switchToggle.InitToggle(defaultGameMngrSfx);
        GameManager.sfx = defaultGameMngrSfx;

        setting_Music.switchToggle.InitToggle(GameManager.music);

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
        if (!wasSelected)
        {
            wasSelected = true;
            SaveLoad_Settings.Instance.SaveSettings(GameManager.sfx, GameManager.music);        
            settingWindow.DOScale(0, openWinDuration).SetEase(closeWindowEase).OnComplete(() =>
            {
                gameObject.SetActive(false);
                wasSelected = false;
            });
        }
    }

}
