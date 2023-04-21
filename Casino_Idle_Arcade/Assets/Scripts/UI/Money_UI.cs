using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Money_UI : MonoBehaviour
{
    [SerializeField] float addMoneyCd;
    [SerializeField] float maxAddMoneyCd;

    public int totalMoney;
    public int currentMoney;
    [SerializeField] Color flashColor;
    Color defaultMoneyIconColor;
    Vector3 iconDefaultScale;

    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] Image moneyIcon;

    public static Money_UI Instance;


    private void Awake()
    {
        if (Instance == null) Instance = this;        
    }

    private void Start()
    {
        iconDefaultScale = moneyIcon.transform.localScale;
        defaultMoneyIconColor = moneyIcon.color;
        currentMoney = GameManager.totalMoney;
        addMoneyCd = maxAddMoneyCd;
        SetMoneyUiTxt();
    }



    private void Update()
    {
        totalMoney = GameManager.totalMoney;

        //if (currentMoney != GameManager.totalMoney)
        //{
        //    addMoneyCd -= Time.deltaTime;
        //    moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
        //    moneyIcon.color = flashColor;



        //    if (addMoneyCd <= 0)
        //    {
        //        if (currentMoney < GameManager.totalMoney)
        //        {
        //            currentMoney++;
        //        }
        //        else if (currentMoney > GameManager.totalMoney)
        //        {
        //            currentMoney--;
        //        }

        //        SetMoneyUiTxt();
        //        moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
        //        moneyIcon.color = flashColor;
        //    }
        //}
        //else
        //{
        //    moneyIcon.transform.localScale = iconDefaultScale;
        //    moneyIcon.color = defaultMoneyIconColor;
        //}


        if (currentMoney < GameManager.totalMoney)
        {
            addMoneyCd -= Time.deltaTime;
            moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
            moneyIcon.color = flashColor;

            if (addMoneyCd <= 0)
            {
                currentMoney++;
                SetMoneyUiTxt();
                //moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
                //moneyIcon.color = flashColor;
            }
        }
        else
        {
            moneyIcon.transform.localScale = iconDefaultScale;
            moneyIcon.color = defaultMoneyIconColor;
        }

    }

    public void SetCurrentMoney() => currentMoney = GameManager.totalMoney;

    void SetMoneyUiTxt() => moneyTxt.text = currentMoney.ToString();

    public void SetTotalMoneyTxt()
    {
        currentMoney = GameManager.totalMoney;
        moneyTxt.text = GameManager.totalMoney.ToString();
    }
}
