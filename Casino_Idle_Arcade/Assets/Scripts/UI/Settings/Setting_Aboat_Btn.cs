using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Setting_Aboat_Btn : MonoBehaviour
{

    [SerializeField] float duration;
    [SerializeField] Ease openEase;
    [SerializeField] Ease closeEase;
    float windowDefaultScale;
    bool wasSelected;

    [SerializeField] GameObject aboatPanel;
    [SerializeField] Transform aboatWindow;
    [SerializeField] Button aboatOkBtn;
    [SerializeField] Button btn;

    void Start()
    {
        windowDefaultScale = aboatWindow.localScale.x;
        aboatWindow.localScale = Vector3.zero;

        btn.onClick.AddListener(OpenWindow);
        aboatOkBtn.onClick.AddListener(CloseWindow);

    }

    

    void OpenWindow()
    {
        aboatPanel.SetActive(true);
        aboatWindow.DOScale(windowDefaultScale, duration).SetEase(openEase);
        AudioSourceManager.Instance.OpenWindowUi();

    }

    void CloseWindow()
    {
        if (!wasSelected)
        {
            AudioSourceManager.Instance.CloseWindowUi();
            wasSelected = true;
            aboatWindow.DOScale(0, duration).SetEase(closeEase).OnComplete(() =>
            {
                aboatPanel.SetActive(false);
                wasSelected = false;
            });

        }
    }

}
