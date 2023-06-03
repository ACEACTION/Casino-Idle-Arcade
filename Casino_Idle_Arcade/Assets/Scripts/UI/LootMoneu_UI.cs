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
        mainPanel.localScale = new Vector3(.4f, .4f, .4f);
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
        int startAmount = 0;
        int targetAmount = moneyAmount;
        float duration = .5f; // The duration of the animation in seconds

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startAmount, targetAmount, progress));
            SetUITxt(currentValue.ToString());
            yield return null;
        }

        SetUITxt(targetAmount.ToString());
    }

    void SetUITxt(string txt) => lootMoneyTxt.text = string.Concat("$", txt);

}
