using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;

public class CasinoChipGameCanvas : MonoBehaviour
{

    [SerializeField] CasinoChipGameCanvasData data;

    [Header("Chip Panel")]
    [SerializeField] Transform chipPanel;
    Vector3 chipPanelScale;
    bool chipPanelIsOpen;

    [Header("Clean Panel")]
    [SerializeField] Transform cleanSlider;
    Vector3 cleanPanelScale;
    bool cleanPanelIsOpen;


    [Header("Play Game Panel")]
    [SerializeField] Transform playGameSlider;
    Vector3 playGamePanelScale;
    bool playGamePanelIsOpen;

    //Vector3 originPos;

    private void Start()
    {
        data.originPos = chipPanel.localPosition;
        GetDefaultScale();
    }

    void GetDefaultScale()
    {
        chipPanelScale = chipPanel.localScale;
        cleanPanelScale = cleanSlider.localScale;
        playGamePanelScale = playGameSlider.localScale;
    }


    public void OpenChipPanel()
    {
        if (!chipPanelIsOpen)
        {
            chipPanelIsOpen = true;
            OpenPanel(chipPanel, chipPanelScale);
        }
    }

    public void CloseChipPanel()
    {
        if (chipPanelIsOpen)
        {        
            chipPanelIsOpen = false;
            ClosePanel(chipPanel);
        }
    }
    public void OpenCleanPanel()
    {
        if (!cleanPanelIsOpen)
        {
            cleanPanelIsOpen = true;
            OpenPanel(cleanSlider, cleanPanelScale);
        }
    }
    public void CloseCleanPanel()
    {
        if (cleanPanelIsOpen)
        {
            cleanPanelIsOpen = false;
            ClosePanel(cleanSlider); 
        }
    }
    public void OpenPlayGamePanel()
    {
        if (!playGamePanelIsOpen)
        { 
            playGamePanelIsOpen = true;
            OpenPanel(playGameSlider, playGamePanelScale); 
        }
    }
    public void ClosePlayGamePanel()
    {
        if (playGamePanelIsOpen)
        {
            playGamePanelIsOpen = false;
            ClosePanel(playGameSlider);
        }
    }


    void OpenPanel(Transform panel, Vector3 defaultScale)
    {
        panel.localPosition = data.originPos;
        panel.localScale = Vector3.zero;
        panel.gameObject.SetActive(true);
        panel.DOLocalMoveY(panel.transform.localPosition.y + data.yMoveTarget, data.duration);
        panel.DOScale(defaultScale, data.duration);
    }

    void ClosePanel(Transform panel)
    {
        panel.DOScale(0, data.duration);
        panel.DOLocalMove(data.originPos, data.duration).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);
        });
        

    }

}
