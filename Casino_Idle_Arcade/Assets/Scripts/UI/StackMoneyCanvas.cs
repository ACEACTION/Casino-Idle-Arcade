using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StackMoneyCanvas : MonoBehaviour
{
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
        moneyText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        moneyText.transform.localPosition = defaultPos;
        moneyText.gameObject.SetActive(false);
    }


    public void AddMoneyText(int moneyAmount)
    {
        moneyText.text = string.Concat("$", moneyAmount.ToString());
        moneyText.gameObject.SetActive(true);
        moneyText.transform.DOScale(defaultScale, activeDuration).SetEase(easeType)
        .OnComplete(() =>
        {
            moneyText.transform.DOLocalMoveY(.7f, .7f).SetEase(easeType).OnComplete(() =>
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
