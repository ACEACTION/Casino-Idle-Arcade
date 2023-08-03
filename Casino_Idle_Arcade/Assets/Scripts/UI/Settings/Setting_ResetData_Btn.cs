using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Setting_ResetData_Btn : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Ease openEase;
    [SerializeField] Ease closeEase;
    float defaultWindowScale;
    bool wasSelected; 
    [SerializeField] Button btn;
    [SerializeField] Button resetPanelNoBtn;
    [SerializeField] GameObject resetDataPanel;
    [SerializeField] Transform resetPanelWindow;
    
    void Start()
    {
        defaultWindowScale = resetPanelWindow.localScale.x;
        resetPanelWindow.transform.localScale = Vector3.zero;

        btn.onClick.AddListener(OpenPanel);
        resetPanelNoBtn.onClick.AddListener(ClosePanel);
    }


    void OpenPanel()
    {
        resetDataPanel.SetActive(true);
        resetPanelWindow.DOScale(defaultWindowScale, duration).SetEase(openEase);
        AudioSourceManager.Instance.OpenWindowUi();
    }

    void ClosePanel()
    {
        if (!wasSelected)
        {
            wasSelected = true;
            AudioSourceManager.Instance.CloseWindowUi();
            resetPanelWindow.DOScale(0, duration).SetEase(closeEase).OnComplete(() =>
            {
                resetDataPanel.SetActive(false);
                wasSelected = false;
            });
        }
    }

}
