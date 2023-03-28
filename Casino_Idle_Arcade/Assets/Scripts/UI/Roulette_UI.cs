using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roulette_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chipTxt;
    [SerializeField] GameObject fatherPanel;
    [SerializeField] GameObject chipPanel;
    [SerializeField] GameObject playingGamePanel;

    public void SetChipTxt(string txt) => chipTxt.text = txt;    
    public void SetChipPanelState(bool state) => chipPanel.SetActive(state);
    public void SetFatherPanelState(bool state) => fatherPanel.SetActive(state);
    public void SetPlayingGamePanelState(bool state) => playingGamePanel.SetActive(state);

}
