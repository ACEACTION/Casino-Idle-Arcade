using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Sfx : MonoBehaviour
{
    
    public Toggle toggle;

    public static Setting_Sfx Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        toggle.onValueChanged.AddListener(ToggleOnChanged);
    }

    void ToggleOnChanged(bool on)
    {
        AudioSourceManager.Instance.PlaySetringBtnChangedSfx();
    }

}
