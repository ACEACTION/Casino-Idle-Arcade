using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Music : MonoBehaviour
{
    public Toggle toggle;

    public static Setting_Music Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        toggle.onValueChanged.AddListener(ToggleOnChanged);
        ToggleOnChanged(toggle.isOn);
    }

    void ToggleOnChanged(bool on)
    {
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
