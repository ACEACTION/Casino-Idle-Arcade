using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainUpgradePanel : MonoBehaviour
{
    public Button closeBtn;
    public Transform mainWindow;
    public static MainUpgradePanel Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

   
    

}
