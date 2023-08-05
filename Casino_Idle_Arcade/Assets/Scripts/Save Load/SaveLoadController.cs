using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class SaveLoadController : MonoBehaviour
{


    // paths
    const string tutorialStatePath = "toturialStatePath";


    // refs
    [SerializeField] SaveLoad_Settings saveLoad_settings;

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
    }


    private void Start()
    {
        LoadTutorialState();
        saveLoad_settings.LoadSettings();
        GameManager.LoadTotalMoney(defaultMoney);
    }

    public void SaveTutorialState()
    {
        SaveLoadSystem.SaveAes(GameManager.isCompleteTutorial, tutorialStatePath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });

        saveLoad_settings.SaveSettings(true, true); 
    }

    void LoadTutorialState()
    {
        SaveLoadSystem.LoadAes<bool>((data) => {
            GameManager.isCompleteTutorial = true;
        }, tutorialStatePath
        , (error) => {
            //GameManager.isCompleteTutorial = false;
            GameManager.isCompleteTutorial = isCompleteTutorial;
        }
        , (success) => { Debug.Log(success); });
    }

    public void DeleteTutorialStateFile()
    {
        SaveLoadSystem.DeleteFile(tutorialStatePath);
    }

}
