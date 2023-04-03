using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyTxt;

    public static Money_UI Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SetMoneyTxt()
    {
        moneyTxt.text = GameManager.totalMoney.ToString();
    }

}
