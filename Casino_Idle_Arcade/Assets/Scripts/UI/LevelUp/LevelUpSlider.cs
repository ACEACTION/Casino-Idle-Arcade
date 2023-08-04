using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelUpSlider : MonoBehaviour
{

    int totalMoney;

    [SerializeField] int sliderUnit;

    [SerializeField] Slider slider;
    [SerializeField] LevelUpData data;
    [SerializeField] TextMeshProUGUI lvlTxt;
    Camera cam;
    [SerializeField] Canvas canvas;

    bool revertPos = true;

    [Header("Money")]
    [SerializeField] float moneyDuration;
    [SerializeField] Transform totalMoneyIcon;
    [SerializeField] Button money;
    [SerializeField] GameObject[] moneyClones;
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] float moveY;
    [SerializeField] float midPathOffset;
    Vector3 moneyTxtOriginPos;

    [Header("Stars")]
    [SerializeField] float starsDuration;
    [SerializeField] float starRevertFirstPosAmount;
    [SerializeField] float startMidPathAmount;
    [SerializeField] RectTransform starIcon;
    [SerializeField] RectTransform[] starsClone;

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
        cam = Camera.main;

        moneyTxtOriginPos = moneyTxt.transform.position;
        moneyTxt.gameObject.SetActive(false);

    }

    void SetMoneyScale() =>
        money.transform.DOScale(money.transform.localScale.x + .1f, .6f).SetLoops(-1, LoopType.Yoyo);


    void MoneyAction()
    {

        MoveMoneyClones();
        MoveMoneyTxt();

    }

    void MoveMoneyClones()
    {
        money.gameObject.SetActive(false);

        for (int i = 0; i < moneyClones.Length; i++)
        {
            Transform moneyCloneTransform = moneyClones[i].transform;
            moneyCloneTransform.gameObject.SetActive(true);
            Vector3 originPos = moneyCloneTransform.position;

            //moneyCloneTransform.DOScale(moneyCloneTransform.localScale.x + .2f, moneyDuration / 2).OnComplete(() =>
            //{
            //    moneyCloneTransform.DOScale(.3f, moneyDuration / 2);
            //});

            moneyCloneTransform.transform.localScale = new Vector3(.4f, .4f, .4f);
            MakeObjectScale(moneyCloneTransform,
                1, .2f, moneyDuration / 2);

            SetDoPath(moneyCloneTransform, totalMoneyIcon.position, midPathOffset, () =>
            {
                totalMoney = 0;
                moneyCloneTransform.DOKill();
                moneyCloneTransform.position = originPos;
                moneyCloneTransform.localScale = Vector3.one;
                GameManager.AddMoney(50);
                Money_UI.Instance.SetTotalMoneyTxt();
            });

        }
    }

    void MoveMoneyTxt() 
    {
        moneyTxt.text = string.Concat("+", totalMoney);
        moneyTxt.gameObject.SetActive(true);
        moneyTxt.transform.DOMoveY(moneyTxt.transform.position.y + moveY, moneyDuration).OnComplete(() =>
        {
            moneyTxt.transform.position = moneyTxtOriginPos;
            moneyTxt.gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetLevelUp(PlayerMovements.Instance.transform.position);            
        }

    }



    bool lvlUped = false;
    public void SetLevelUp(Vector3 worldPos)
    {
        data.lvlUpCurrentUnit += sliderUnit;
        StartCoroutine(Delay());
        //slider.value += sliderUnit;
        //if (data.lvlUpCurrentUnit >= slider.maxValue)
        //{
        //    int remain = data.lvlUpCurrentUnit - data.maxLvlUpUnit[data.lvlUpCounter];
        //    if (remain > 0)
        //    {
        //        slider.value = remain;
        //        data.lvlUpCurrentUnit = remain;
        //    }
        //    else
        //    {
        //        slider.value = 0;
        //        data.lvlUpCurrentUnit = 0;
        //    }

        //    data.lvlUpCounter++;
        //    SetSliderMaxValue();
        //    money.gameObject.SetActive(true);
        //    totalMoney += data.GetMoneyAmountPerLevelUp();
        //    lvlUped = true;

        //    SetLvlTxt();
        //}


        //if (makeStars)
        MakeStars(worldPos);

    }

    IEnumerator Delay()
    { 
        yield return new WaitForSeconds(starsDuration);

        slider.value += sliderUnit;
        if (data.lvlUpCurrentUnit >= slider.maxValue)
        {
            int remain = data.lvlUpCurrentUnit - data.maxLvlUpUnit[data.lvlUpCounter];
            if (remain > 0)
            {
                slider.value = remain;
                data.lvlUpCurrentUnit = remain;
            }
            else
            {
                slider.value = 0;
                data.lvlUpCurrentUnit = 0;
            }

            data.lvlUpCounter++;
            SetSliderMaxValue();
            money.gameObject.SetActive(true);
            totalMoney += data.GetMoneyAmountPerLevelUp();
            lvlUped = true;

            SetLvlTxt();
        }
    }


    void SetSliderValue()
    {
        //slider.value += sliderUnit;

        if (data.lvlUpCurrentUnit >= slider.maxValue)
        {
            print("VC");
            data.lvlUpCounter++;
            int remain = data.lvlUpCurrentUnit - data.maxLvlUpUnit[data.lvlUpCounter];
            if (remain > 0)
            {
                slider.value = remain;
                data.lvlUpCurrentUnit = remain;
            }
            else
            {
                slider.value = 0;
                data.lvlUpCurrentUnit = 0;
            }

            SetSliderMaxValue();
            money.gameObject.SetActive(true);
            totalMoney += data.GetMoneyAmountPerLevelUp();
            lvlUped = true;

            SetLvlTxt();
        }
    }

    
    void MakeStars(Vector3 worldPos)
    {        
        Vector2 screenPos = GetScreenPos(worldPos);
        for (int i = 0; i < starsClone.Length; i++)
        {
            Transform star = starsClone[i];
            star.gameObject.SetActive(true);
            SetFirstStarClonePos(star, screenPos, starRevertFirstPosAmount);

            SetDoPath(star, starIcon.position, startMidPathAmount, ()=> {
                MakeStarIconScale();
                //slider.value += sliderUnit;
                //SetSliderValue();
            });

            star.localScale = Vector3.zero;

            star.DOScale(1.2f, starsDuration / 2).OnComplete(() =>
            {
                star.DOScale(1, starsDuration / 2);
            });

            revertPos = !revertPos;
        }
        
    }


    void SetFirstStarClonePos(Transform clone, Vector2 centerPos, float offset)
    {
        if (revertPos)
            clone.position = centerPos + new Vector2(Random.Range(0, offset), Random.Range(0, offset));
        else
            clone.position = centerPos + new Vector2(Random.Range(0, -offset), Random.Range(0, offset));

    }

    void SetDoPath(Transform clone, Vector3 destination, float midOffset, System.Action completeAction)
    {
        Vector2 midPath = (clone.position + destination) / 2;

        //midPath.x += Random.Range(-startMidPathAmount, startMidPathAmount);

        if (!revertPos)
        {
            midPath.x += Random.Range(0, midOffset);
        }
        else
            midPath.x -= Random.Range(0, midOffset);


        Vector3[] path = { clone.position, midPath, destination};
        clone.DOPath(path, starsDuration, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(() =>
        {
            //MakeStarIconScale();
            completeAction();
            clone.gameObject.SetActive(false);
        });
    }

    void MakeStarIconScale() 
        => MakeObjectScale(starIcon, starIcon.localScale.x + .2f, starIcon.localScale.x, .2f);

    void MakeObjectScale(Transform t, float targetScale, float originScale, float duration) =>
        t.DOScale(targetScale, duration).OnComplete(() => { t.DOScale(originScale, duration); });

    Vector2 GetScreenPos(Vector3 worldPos) => RectTransformUtility.WorldToScreenPoint(cam, worldPos);

    void SetSliderMaxValue() => slider.maxValue = data.maxLvlUpUnit[data.lvlUpCounter];
    void SetLvlTxt() => lvlTxt.text = string.Concat("lvl ", data.lvlUpCounter + 1);

    private void OnDisable()
    {
        data.lvlUpCounter = 0;
        data.lvlUpCurrentUnit = 0;
    }

}
