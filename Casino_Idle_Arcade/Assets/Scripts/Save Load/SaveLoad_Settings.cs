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
                Debug.Log(success);
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
                AudioSourceBgMusic.Instance.SetAudioSource();
            }, dataPath
            , (error) => { Debug.Log(error); }
            , (success) => { Debug.Log(success); });
        }
        else
        {
            // player doesnt play game even, firs time (or player didn't complete tutorial)
            GameManager.sfx = true;
            GameManager.music = true;
            AudioSourceBgMusic.Instance.SetAudioSource();
        }
    }

    public void DeleteSettingDataFile()
    {
        SaveLoadSystem.DeleteFile(dataPath);
    }

}

[System.Serializable]
public class SettingsSaveData
{
    public bool sfx;
    public bool music;
}