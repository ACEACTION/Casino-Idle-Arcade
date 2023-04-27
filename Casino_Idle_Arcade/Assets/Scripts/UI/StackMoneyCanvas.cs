using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StackMoneyCanvas : MonoBehaviour
{
    [SerializeField] GameObject moneyPanel;
    [SerializeField] TextMeshProUGUI moneyText;
    Vector3 defaultScale;
    public Vector3 defaultPos;
    public Ease easeType;
    public float activeDuration;

    private void Start()
    {
        defaultScale = moneyText.transform.localScale;
        defaultPos = moneyText.transform.localPosition;
        ResetTxt();
    }

    void ResetTxt()
    {
        moneyPanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        moneyPanel.transform.localPosition = defaultPos;
        moneyPanel.SetActive(false);
    }


    public void AddMoneyText(int moneyAmount)
    {
        moneyText.text = string.Concat("+", moneyAmount.ToString());
        moneyPanel.SetActive(true);
        moneyPanel.transform.DOScale(defaultScale, activeDuration).SetEase(easeType)
        .OnComplete(() =>
        {
            moneyPanel.transform.DOLocalMoveY(.7f, .7f).SetEase(easeType).OnComplete(() =>
            {
                StartCoroutine(DeactiveTxt());
            });
        });
    }

    IEnumerator DeactiveTxt()
    {
        yield return new WaitForSeconds(1);
        ResetTxt();
    }

}
