using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LootMoneu_UI : MonoBehaviour
{
    [SerializeField] float moveYtarget;
    [SerializeField] float duration;
    [SerializeField] Ease moveEaseType;

    Vector3 originPos;
    Vector3 defaultScale;
    [SerializeField] TextMeshProUGUI lootMoneyTxt;
    [SerializeField] Transform mainPanel;

    public static LootMoneu_UI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        originPos = mainPanel.position;
        defaultScale = mainPanel.localScale;

        mainPanel.localScale = new Vector3(.01f, .01f, .01f);
        mainPanel.gameObject.SetActive(false);
    }

    public void ShowLootMoneyTxt(int moneyAmount)
    {
        mainPanel.gameObject.SetActive(true);
        mainPanel.transform.DOKill();
        //lootMoneyTxt.text = moneyAmount.ToString();
        mainPanel.localScale = new Vector3(.01f, .01f, .01f);
        mainPanel.position = originPos;

        mainPanel.DOMoveY(transform.position.y + moveYtarget, duration)
            .OnComplete(() =>
            {
                mainPanel.gameObject.SetActive(false);
            });

        mainPanel.transform.DOScale(defaultScale, duration).SetEase(moveEaseType);
        
        
        StartCoroutine(IncreaseMoneyProgress(moneyAmount));
    }

    IEnumerator IncreaseMoneyProgress(int moneyAmount)
    {
        yield return new WaitForSeconds(.05f);

        lootMoneyTxt.text = moneyAmount.ToString();

    }

}
