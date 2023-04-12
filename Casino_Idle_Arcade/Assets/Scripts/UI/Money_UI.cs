using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Money_UI : MonoBehaviour
{
    [SerializeField] Color flashColor;
    Color defaultMoneyIconColor;
    Vector3 iconDefaultScale;

    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] Image moneyIcon;

    public static Money_UI Instance;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        SetMoneyTxt();

    }

    private void Start()
    {
        iconDefaultScale = moneyIcon.transform.localScale;
        defaultMoneyIconColor = moneyIcon.color;
    }

    public void SetMoneyTxt()
    {
        moneyTxt.text = GameManager.totalMoney.ToString();
        StartCoroutine(ShakeMoneyIcon());
    }

    
    IEnumerator ShakeMoneyIcon()
    {
        moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
        moneyIcon.color = flashColor;
        yield return new WaitForSeconds(.1f);
        moneyIcon.transform.localScale = iconDefaultScale;
        moneyIcon.color = defaultMoneyIconColor;

    }

}
