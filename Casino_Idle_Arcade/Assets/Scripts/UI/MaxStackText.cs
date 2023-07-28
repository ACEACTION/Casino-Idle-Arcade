using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxStackText : MonoBehaviour
{
    [SerializeField] GameObject maxTxt;
    public static MaxStackText Instance;    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetTextState(bool state) => maxTxt.SetActive(state);
    
    public bool GetTextActiveSelf() => maxTxt.activeSelf;

}
