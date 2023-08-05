using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelUpSlider : MonoBehaviour
{

    //int totalMoney;

    [SerializeField] int sliderUnit;

    [SerializeField] Slider slider;
    public LevelUpData data;
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
    [SerializeField] float starFirsScaleDuration;
    [SerializeField] float starRotDuration;
    [SerializeField] float doPathDelay;
    [SerializeField] float starRotate;
    [SerializeField] float starsDuration;
    [SerializeField] float starRevertFirstPosAmount;
    [SerializeField] float startMidPathAmount;
    float starIconDefaultScale;
    float starCloneDefaultScale;
    public RectTransform starIcon;
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
        slider.value = data.lvlUpCurrentUnit;

        SetMoneyScale();
        money.onClick.AddListener(MoneyAction);
        cam = Camera.main;
        
        if (data.totalMoney > 0)
            money.gameObject.SetActive(true);
        else
            money.gameObject.SetActive(false);

        moneyTxtOriginPos = moneyTxt.transform.position;
        moneyTxt.gameObject.SetActive(false);

        starIconDefaultScale = starIcon.localScale.x;
        starCloneDefaultScale = starsClone[0].localScale.x;
    }

    void SetMoneyScale() =>
        money.transform.DOScale(money.transform.localScale.x + .1f, .6f).SetLoops(-1, LoopType.Yoyo);


    void MoneyAction()
    {

        money.gameObject.SetActive(false);
        //MoveMoneyClones();
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
                moneyCloneTransform.DOKill();
                moneyCloneTransform.position = originPos;
                moneyCloneTransform.localScale = Vector3.one;
                GameManager.AddMoney(data.totalMoney);
                Money_UI.Instance.SetTotalMoneyTxt();
                data.totalMoney = 0;
            });

        }
    }

    void MoveMoneyTxt()
    {
        GameManager.AddMoney(data.totalMoney);
        Money_UI.Instance.SetTotalMoneyTxt();

        moneyTxt.text = string.Concat("+", data.totalMoney);
        moneyTxt.gameObject.SetActive(true);
        moneyTxt.transform.DOMoveY(moneyTxt.transform.position.y + moveY, moneyDuration).OnComplete(() =>
        {
            moneyTxt.transform.position = moneyTxtOriginPos;
            moneyTxt.gameObject.SetActive(false);
            data.totalMoney = 0;
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetLevelUp(PlayerMovements.Instance.transform.position);            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < starsClone.Length; i++)
            {
                starsClone[i].localScale = Vector3.zero;
            }
        }

    }



    public void SetLevelUp(Vector3 worldPos)
    {
        StartCoroutine(FillSliderValue());
        MakeStars(worldPos);

    }

    IEnumerator FillSliderValue()
    {
        yield return new WaitForSeconds(starsDuration + doPathDelay * starsClone.Length * 2);

        if (!LvlIsMax())
        {
            data.lvlUpCurrentUnit += sliderUnit;
            //slider.value += sliderUnit;

            if (data.lvlUpCurrentUnit >= slider.maxValue)
            {
                int remain = data.lvlUpCurrentUnit - data.maxLvlUpUnit[data.lvlUpCounter];
                if (remain > 0)
                {
                    //slider.value = remain;
                    data.lvlUpCurrentUnit = remain;

                    slider.DOValue(slider.maxValue, .4f).OnComplete(() =>
                    {
                        slider.value = 0;
                        slider.DOValue(remain, .4f);
                    });

                }
                else
                {
                    data.lvlUpCurrentUnit = 0;
                    slider.DOValue(slider.maxValue, .4f).OnComplete(() =>
                    {
                        slider.value = 0;
                    });
                }


                data.lvlUpCounter++;
                if (!LvlIsMax())
                {
                    SetSliderMaxValue();
                }
                else
                {
                    slider.DOKill();
                    slider.DOValue(slider.maxValue, .4f);
                }

                money.gameObject.SetActive(true);
                data.totalMoney += data.GetMoneyAmountPerLevelUp();
                SetLvlTxt();
            }
            else
                slider.DOValue(data.lvlUpCurrentUnit, .4f);
        }
        else
        {
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
            revertPos = !revertPos;
        }

        StartCoroutine(StarsDoPathProcess());
    }

    
    
    IEnumerator StarsDoPathProcess()
    {

        for (int i = 0; i < starsClone.Length; i++)
        {
            starsClone[i].localScale = Vector3.zero;
        }

        for (int i = 0; i < starsClone.Length; i++)
        {
            Transform star = starsClone[i];
            star.transform.DOKill();
            yield return new WaitForSeconds(doPathDelay);                                   
            star.eulerAngles = new Vector3(0, starRotate, 0);
            star.DOScale(starCloneDefaultScale, starFirsScaleDuration).OnComplete(() =>
            {                
                star.DORotate(new Vector3(0, -starRotate, 0), starRotDuration).SetLoops(-1, LoopType.Yoyo);
            });
        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < starsClone.Length; i++)
        {
            Transform star = starsClone[i];
            star.transform.DOKill();
            star.DORotate(Vector3.zero, starRotDuration);
            yield return new WaitForSeconds(doPathDelay);

            MakeObjectScale(star, starCloneDefaultScale + .3f, starCloneDefaultScale / 2, starsDuration / 2);

            SetDoPath(star, starIcon.position, startMidPathAmount, () =>
            {
                MakeStarIconScale();
                star.localScale = Vector3.one;
                star.transform.DOKill();
            });
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
        => MakeObjectScale(starIcon, starIconDefaultScale + .4f, starIconDefaultScale, .2f);

    void MakeObjectScale(Transform t, float targetScale, float originScale, float duration) =>
        t.DOScale(targetScale, duration).OnComplete(() => { t.DOScale(originScale, duration); });

    Vector2 GetScreenPos(Vector3 worldPos) => RectTransformUtility.WorldToScreenPoint(cam, worldPos);

    void SetSliderMaxValue() => slider.maxValue = data.maxLvlUpUnit[data.lvlUpCounter];
    void SetLvlTxt()
    {
        if (LvlIsMax())
            lvlTxt.text = "lvl max";
        else
            lvlTxt.text = string.Concat("lvl ", data.lvlUpCounter + 1);
        
        //MakeObjectScale(lvlTxt.transform, lvlTxt.transform.localScale.x + .2f, lvlTxt.transform.localScale.x, .3f);
        lvlTxt.transform.DOScale(lvlTxt.transform.localScale + new Vector3(.2f, 0, 0), .3f).OnComplete(
            () => { lvlTxt.transform.DOScale(lvlTxt.transform.localScale - new Vector3(.2f, 0, 0), .3f); });
    }

    bool LvlIsMax() => data.lvlUpCounter + 1 > data.maxLvlUpUnit.Count;

    private void OnDisable()
    {
        //data.lvlUpCounter = 0;
        //data.lvlUpCurrentUnit = 0;
    }

}
