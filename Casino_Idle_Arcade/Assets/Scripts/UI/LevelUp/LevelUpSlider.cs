using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelUpSlider : MonoBehaviour
{
    [SerializeField] int sliderUnit;

    [SerializeField] Slider slider;
    [SerializeField] LevelUpData data;
    [SerializeField] TextMeshProUGUI lvlTxt;

    [Header("Money")]
    [SerializeField] float moneyDuration;
    [SerializeField] Transform totalMoneyIcon;
    [SerializeField] Button money;
    [SerializeField] GameObject[] moneyClones;
    


    public static LevelUpSlider Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        SetLvlTxt();
        SetSliderMaxValue();

        SetMoneyScale();
        money.gameObject.SetActive(false);
        money.onClick.AddListener(MoneyAction);


    }

    void SetMoneyScale() =>
        money.transform.DOScale(money.transform.localScale.x + .1f, .6f).SetLoops(-1, LoopType.Yoyo);


    void MoneyAction()
    {
        for (int i = 0; i < moneyClones.Length; i++)
        {
            Transform moneyCloneTransform = moneyClones[i].transform;
            moneyCloneTransform.gameObject.SetActive(true);
            Vector3 originPos = moneyCloneTransform.position;

            moneyCloneTransform.DOScale(moneyCloneTransform.localScale.x + .7f, moneyDuration / 2).OnComplete(() =>
            {
                moneyCloneTransform.DOScale(0f, moneyDuration / 2);
            });

            moneyCloneTransform.transform.DOMove(totalMoneyIcon.position, moneyDuration).OnComplete(() =>
            {
                moneyCloneTransform.gameObject.SetActive(false);
                moneyCloneTransform.DOKill();
                moneyCloneTransform.position = originPos;
                moneyCloneTransform.localScale = Vector3.one;
            });

        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetLevelUp();         
        }
    }

    


    void SetLevelUp()
    {
        data.lvlUpCurrentUnit += sliderUnit;
        slider.value += sliderUnit;

        if (data.lvlUpCurrentUnit >= slider.maxValue)
        {          
            int remain = data.lvlUpCurrentUnit - data.maxLvlUpUnit[data.lvlUpCounter];
            data.lvlUpCurrentUnit = 0;
            if (remain > 0)
                slider.value = remain;
            else
                slider.value = 0;

            data.lvlUpCounter++;
            SetSliderMaxValue();
            money.gameObject.SetActive(true);
        }

        SetLvlTxt();
    }

    void SetSliderMaxValue() => slider.maxValue = data.maxLvlUpUnit[data.lvlUpCounter];
    void SetLvlTxt() => lvlTxt.text = string.Concat("lvl ", data.lvlUpCounter + 1);

    private void OnDisable()
    {
        data.lvlUpCounter = 0;
        data.lvlUpCurrentUnit = 0;
    }

}
