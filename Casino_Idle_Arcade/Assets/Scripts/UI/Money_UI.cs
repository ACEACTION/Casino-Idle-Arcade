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
    public float moneyAmount;
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
        addMoneyCd = maxAddMoneyCd;
        
    }



    private void Update()
    {
        //totalMoney = GameManager.GetTotalMoney();        
        //if (currentMoney < GameManager.GetTotalMoney())
        //{
        //    addMoneyCd -= Time.deltaTime;
        //    moneyIcon.transform.localScale = iconDefaultScale + new Vector3(.3f, .3f, .3f);
        //    moneyIcon.color = flashColor;

        //    if (addMoneyCd <= 0)
        //    {
        //        currentMoney++;
        //        SetMoneyUiTxt();                
        //    }
        //}
        //else
        //{
        //    moneyIcon.transform.localScale = iconDefaultScale;
        //    moneyIcon.color = defaultMoneyIconColor;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
            print(GameManager.GetTotalMoney());

    }

    void SetMoneyUiTxt() => moneyTxt.text = moneyAmount.ToString();

    public void SetTotalMoneyTxt()
    {
        moneyAmount = GameManager.GetTotalMoney();

        if (moneyAmount < 1000)
        {
            SetTxt(moneyAmount.ToString());
        }
        else if (moneyAmount >= 1000 && moneyAmount < 10000)
        {
            moneyAmount /= 1000;
            SetTxt(moneyAmount.ToString("F1") + "k");
        }
        else if (moneyAmount >= 10000 && moneyAmount < 100000)
        {
            moneyAmount /= 1000;
            SetTxt(moneyAmount.ToString("F2") + "k");
        }
        else if (moneyAmount >= 100000 && moneyAmount < 1000000)
        {
            moneyAmount /= 1000;
            SetTxt(moneyAmount.ToString("F2") + "k");
        }
        else if (moneyAmount >= 1000000)
        {
            moneyAmount /= 1000000;
            SetTxt(moneyAmount.ToString("F2") + "m");
        }        
        
            

        //moneyTxt.text = GameManager.GetTotalMoney().ToString();
    }

    void SetTxt(string txt) => moneyTxt.text = txt;



}
