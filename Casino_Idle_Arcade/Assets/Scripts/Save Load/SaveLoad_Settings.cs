using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class SaveLoad_Settings : MonoBehaviour
{

    const string dataPath = "settings";

    public static SaveLoad_Settings Instance;
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

    public void SaveSettings(bool sfx, bool music)
    {
        SettingsSaveData data = new SettingsSaveData()
        {
            sfx = sfx,
            music = music
        };        

        SaveLoadSystem.SaveAes(data, dataPath,
            (error) => { 
                Debug.Log(error);    
            },            
            (success) => {
                //Debug.Log(success);
            });
    }


    public void LoadSettings()
    {
        if (GameManager.isCompleteTutorial)
        {
            SaveLoadSystem.LoadAes<SettingsSaveData>((data) =>
            {
                GameManager.sfx = data.sfx;
                GameManager.music = data.music;

            }, dataPath
            , (error) => {
                SetSettingsDefault();
                Debug.Log(error); 
            }
            , (success) => { //Debug.Log(success);
                            });

            AudioSourceBgMusic.Instance.SetAudioSource();
        }
        else
        {
            // player doesnt play game even, firs time (or player didn't complete tutorial)
            SetSettingsDefault();
        }
    }

    public void DeleteSettingDataFile()
    {
        SetSettingsDefault();
        SaveLoadSystem.DeleteFile(dataPath);
    }

    void SetSettingsDefault()
    {
        GameManager.sfx = true;
        GameManager.music = true;
        SaveSettings(true, true);
        AudioSourceBgMusic.Instance.SetAudioSource();
    }

    int languageIndex;
    public int GetLanguageIndex() => languageIndex;
    public void SetLanguageIndex(int index) => languageIndex = index;
    
}

[System.Serializable]
public class SettingsSaveData
{
    public bool sfx;
    public bool music;
}