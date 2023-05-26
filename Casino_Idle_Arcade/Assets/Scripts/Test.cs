

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class Test : MonoBehaviour
{
    public float vibrateCancelTime;
    public TMP_InputField inputField;

    public void VibrateBtn()
    {
        StartCoroutine(VibrateProcess());
    }

    IEnumerator VibrateProcess()
    {
        //Vibrator.Vibrate((Convert.ToInt64(inputField.text)));
        //yield return new WaitForSeconds(vibrateCancelTime);
        //Vibrator.Cancel();
        yield return new WaitForEndOfFrame();
        Handheld.Vibrate();
    }


}
