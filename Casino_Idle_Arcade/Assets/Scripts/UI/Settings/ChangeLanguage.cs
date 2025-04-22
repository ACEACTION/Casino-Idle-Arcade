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

        // Clear and populate dropdown
        //languageDropdown.ClearOptions();
        //List<string> options = new List<string>();

        //foreach (var locale in locales)
        //{
        //    options.Add(locale.Identifier.CultureInfo.NativeName);
        //}

        //languageDropdown.AddOptions(options);

        // Set current selected value
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
    }
}
