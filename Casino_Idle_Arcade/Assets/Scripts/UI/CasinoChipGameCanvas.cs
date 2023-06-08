using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;

public class CasinoChipGameCanvas : MonoBehaviour
{    
    [SerializeField] float duration;
    [SerializeField] float yMoveTarget;
    
    Vector3 originPos;

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



    private void Start()
    {
        originPos = chipPanel.position;
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
        print(1);
        if (!chipPanelIsOpen)
        {
            print(2);
            chipPanelIsOpen = true;
            OpenPanel(chipPanel, chipPanelScale);
        }
    }

    public void CloseChipPanel()
    {
        print(3);
        if (chipPanelIsOpen)
        {
            print(4);
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
        if (!cleanPanelIsOpen)
        {
            cleanPanelIsOpen = true;
            ClosePanel(cleanSlider); 
        }
    }
    public void OpenPlayGamePanel()
    {
        if (playGamePanelIsOpen)
        { 
            playGamePanelIsOpen = false;
            OpenPanel(playGameSlider, playGamePanelScale); 
        }
    }
    public void ClosePlayGamePanel() => ClosePanel(playGameSlider);


    void OpenPanel(Transform panel, Vector3 defaultScale)
    {
        panel.position = originPos;
        panel.localScale = Vector3.zero;
        panel.gameObject.SetActive(true);
        panel.DOMoveY(panel.transform.position.y + yMoveTarget, duration);
        panel.DOScale(defaultScale, duration);
    }

    void ClosePanel(Transform panel)
    {
        panel.DOScale(0, duration);
        panel.DOMove(originPos, duration).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);
        });
        

    }

}
