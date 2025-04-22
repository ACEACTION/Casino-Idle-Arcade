using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using RTLTMPro;
using System.Xml;

public class ItemLevelText : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro lvlText;
    [SerializeField] LocalizeStringEvent localizeStringEvent;
    

    string level;

    void Start()
    {
        localizeStringEvent.OnUpdateString.AddListener(OnLocalizedTextChanged);
    }

    void OnLocalizedTextChanged(string localizedValue)
    {
        string combinedText = localizedValue + " " + level;
        lvlText.text = combinedText;
    }

    public void SetText(string level)
    {
        this.level = level;
        lvlText.text = string.Concat(localizeStringEvent.StringReference.GetLocalizedString(), " ", level);
    }

}
