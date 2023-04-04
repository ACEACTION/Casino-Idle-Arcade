using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roulette_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chipTxt;
    [SerializeField] GameObject chipPanel;

    public void SetChipTxt(string txt) => chipTxt.text = txt;    
    public void SetChipPanelState(bool state) => chipPanel.SetActive(state);       

}
