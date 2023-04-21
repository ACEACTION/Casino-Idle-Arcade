using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StackMoneyCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    Vector3 defaultScale;
    public Ease easeType;
    public float activeDuration;

    private void Start()
    {
        defaultScale = moneyText.transform.localScale;
        ResetTxt();
    }

    void ResetTxt()
    {
        moneyText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        moneyText.gameObject.SetActive(false);
    }


    public void AddMoneyText(int moneyAmount)
    {
        moneyText.text = string.Concat("$", moneyAmount.ToString());
        moneyText.gameObject.SetActive(true);
        moneyText.transform.DOScale(defaultScale, activeDuration).SetEase(easeType)
        .OnComplete(() =>
        {            
            StartCoroutine(DeactiveTxt());
        });
    }

    IEnumerator DeactiveTxt()
    {
        yield return new WaitForSeconds(1);
        ResetTxt();
    }

}
