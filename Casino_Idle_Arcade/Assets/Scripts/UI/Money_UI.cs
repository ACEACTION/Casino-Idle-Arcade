using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Money_UI : MonoBehaviour
{
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeStrength;
    Vector3 defaultPos;
    int moneyAmount;
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
        defaultPos = transform.position;
        SetTotalMoneyTxt();
    }



    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
            print(GameManager.GetTotalMoney());

    }

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

        StartCoroutine(SetFlashColor());

    }

    void SetTxt(string txt) => moneyTxt.text = txt;

    IEnumerator SetFlashColor()
    {
        moneyIcon.color = flashColor;
        yield return new WaitForSeconds(.2f);
        moneyIcon.color = defaultMoneyIconColor;
    }

    public void ShakeMoneyUI()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength).OnComplete(() =>
        {
            transform.position = defaultPos;
        });
    }

}
