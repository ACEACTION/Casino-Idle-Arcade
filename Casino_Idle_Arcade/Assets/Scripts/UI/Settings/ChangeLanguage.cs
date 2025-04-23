using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropdown;
    List<Locale> locales;


    void Start()
    {
        // Get available locales
        locales = LocalizationSettings.AvailableLocales.Locales;
        languageDropdown.value = GetCurrentLocaleIndex();
        languageDropdown.RefreshShownValue();

        // Add listener
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    int GetCurrentLocaleIndex()
    {
        var current = LocalizationSettings.SelectedLocale;
        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i].Identifier == current.Identifier)
                return i;
        }
        return 0;
    }

    void OnLanguageChanged(int index)
    {
        LocalizationSettings.SelectedLocale = locales[index];

        SaveLoadController.Instance.SaveLanguageIndex(index);
    }
}
