using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;
using System;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class SaveLoadController : MonoBehaviour
{
    // Keys
    const string tutorialStatePath = "tutorialStatePath";
    const string languageIndexKey = "languageIndexKey";

    // refs
    [SerializeField] SaveLoad_Settings saveLoad_settings;
    [SerializeField] StartLoaderSceneDialogue startLoaderDialogue;

    [Header("Test")]
    public bool isCompleteTutorial;
    public int defaultMoney;


    public static SaveLoadController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadTutorialState();
    }


    void Start()
    {
        saveLoad_settings.LoadSettings();
        GameManager.LoadTotalMoney(defaultMoney);
        LoadLanguageIndex();
    }

    public void SaveTutorialState()
    {
        SaveLoadSystem.SaveAes(GameManager.isCompleteTutorial, tutorialStatePath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
               // Debug.Log(success);
                saveLoad_settings.SaveSettings(true, true); 
            });

    }

    void LoadTutorialState()
    {
        SaveLoadSystem.LoadAes<bool>((data) => {
            GameManager.isCompleteTutorial = data;
        }, tutorialStatePath
        , (error) => {
            GameManager.isCompleteTutorial = isCompleteTutorial;

            if (!isCompleteTutorial)
            {
                GameManager.DeleteTotalMoneyFile();
            }
        }
        , (success) => { 
           // Debug.Log(success); 
        });
    }

    public void DeleteTutorialStateFile()
    {
        SaveLoadSystem.DeleteFile(tutorialStatePath);
    }

    public void SaveLanguageIndex(int languageIndex)
    {
        SaveLoadSystem.SaveAes(languageIndex, languageIndexKey,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log("language saved succeed");
                saveLoad_settings.SetLanguageIndex(languageIndex);
            });
    }

    public void LoadLanguageIndex()
    {
        SaveLoadSystem.LoadAes<int>((data) => {
            List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;
            LocalizationSettings.SelectedLocale = locales[data];
            saveLoad_settings.SetLanguageIndex(data);
            startLoaderDialogue.CallDialogue();
        }, languageIndexKey
       , (error) => {
            Debug.Log(error);
       }
       , (success) => {
       });

       
    }

}
