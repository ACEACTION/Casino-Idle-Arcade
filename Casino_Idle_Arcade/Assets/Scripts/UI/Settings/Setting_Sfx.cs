using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Sfx : MonoBehaviour
{
    public SwitchToggle switchToggle;

    public static Setting_Sfx Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    void Start()
    {
        switchToggle.toggle.onValueChanged.AddListener(ToggleOnChanged);
        //ToggleOnChanged(GameManager.sfx);
        switchToggle.InitToggle(GameManager.sfx);
    }

    void ToggleOnChanged(bool on)
    {
        GameManager.sfx = on;    
        if (!on)
            AudioSourceManager.Instance.PlaySetringBtnChangedSfx();
    }

}
