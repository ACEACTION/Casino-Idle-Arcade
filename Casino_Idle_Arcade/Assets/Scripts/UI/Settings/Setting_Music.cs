using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Music : MonoBehaviour
{
    public SwitchToggle switchToggle;

    public static Setting_Music Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    void Start()
    {
        switchToggle.InitToggle(GameManager.music);
        switchToggle.toggle.onValueChanged.AddListener(ToggleOnChanged);
        //ToggleOnChanged(switchToggle.toggle.isOn);
    }

    void ToggleOnChanged(bool on)
    {
        GameManager.music = on;
        AudioSourceManager.Instance.PlaySetringBtnChangedSfx();
        if (on)
        {
            AudioSourceBgMusic.Instance.audioSource.Play();
            AudioSourceAmbienceMusic.Instance.audioScr.Play();
        }
        else
        {
            AudioSourceBgMusic.Instance.audioSource.Pause();
            AudioSourceAmbienceMusic.Instance.audioScr.Pause();
        }

    }



}
