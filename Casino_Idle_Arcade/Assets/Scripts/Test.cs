

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;
using RDG;

public class Test : MonoBehaviour
{
    public float vibrateCancelTime;
    public TMP_InputField inputField;
    public TMP_InputField inputField2;


    public void VibrateBtn()
    {
        //StartCoroutine(VibrateProcess());
        Vibration.Vibrate(Convert.ToInt64(inputField.text),
                Convert.ToInt32(inputField2.text));

    }

    IEnumerator VibrateProcess()
    {
        Vibrator.Vibrate((Convert.ToInt64(inputField.text)));
        yield return new WaitForSeconds(0.1f);
        Vibrator.Cancel();

    }


}
