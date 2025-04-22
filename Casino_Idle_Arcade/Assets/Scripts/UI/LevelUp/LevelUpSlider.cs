using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class LevelUpSlider : MonoBehaviour
{
    public LevelUpData data;
    [SerializeField] string lvlKey;
    [SerializeField] string maxLvlKey;
    [SerializeField] float sliderFillDuration;
    [SerializeField] LocalizeStringEvent localizeStringEvent;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI lvlTxt;

    [Header("Money")]
    [SerializeField] float moneyDuration;
    [SerializeField] Transform totalMoneyIcon;
    [SerializeField] Button money;
    [SerializeField] GameObject[] moneyClones;
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] float moveY;
    [SerializeField] float midPathOffset;

    [Header("Stars")]
    public RectTransform starIcon;
    [SerializeField] float starFirsScaleDuration;
    [SerializeField] float starRotDuration;
    [SerializeField] float doPathDelay;
    [SerializeField] float starRotate;
    [SerializeField] float starsDuration;
    [SerializeField] float starRevertFirstPosAmount;
    [SerializeField] float startMidPathAmount;
    [SerializeField] RectTransform[] starsClone;

    Vector3 moneyTxtOriginPos;
    float starIconDefaultScale;
    float starCloneDefaultScale;
    Camera cam;
    int currentLevel;
    bool revertPos = true;
    public static LevelUpSlider Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (!GameManager.isCompleteTutorial)
        {
            data.ResetData();
        }

        data.LoadData();

        SetLvlTxtAnimation();
        if (LvlIsMax())
        {
            slider.value = slider.maxValue;
        }
        else
        {
            SetSliderMaxValue();
            slider.value = data.lvlUpCurrentValue;
        }

        SetMoneyScale();
        money.onClick.AddListener(MoneyAction);
        cam = Camera.main;

        if (data.totalMoney > 0 && !LvlIsMax())
            money.gameObject.SetActive(true);
        else
            money.gameObject.SetActive(false);

        moneyTxtOriginPos = moneyTxt.transform.position;
        moneyTxt.gameObject.SetActive(false);

        starIconDefaultScale = starIcon.localScale.x;
        starCloneDefaultScale = starsClone[0].localScale.x;

        localizeStringEvent.OnUpdateString.AddListener(OnLocalizedTextChanged);
    }

    void SetMoneyScale() =>
        money.transform.DOScale(money.transform.localScale.x + .1f, .6f).SetLoops(-1, LoopType.Yoyo);


    void MoneyAction()
    {

        money.gameObject.SetActive(false);
        MoveMoneyTxt();
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

            data.SaveData();
        });
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
            data.lvlUpCurrentValue += data.lvlUpUnit;
            if (data.lvlUpCurrentValue >= data.maxLvlUpUnit[data.lvlUpCounter].maxLvlValue)
            {
                int remain = data.lvlUpCurrentValue - data.maxLvlUpUnit[data.lvlUpCounter].maxLvlValue;
                data.lvlUpCurrentValue = remain;

                data.lvlUpCounter++;
                SetLvlTxtAnimation();

                money.gameObject.SetActive(true);
                data.SetTotalMoney();

                if (!LvlIsMax())
                {

                    slider.DOValue(slider.maxValue, sliderFillDuration).OnComplete(() =>
                    {
                        SetSliderMaxValue();
                        slider.value = 0;
                        slider.DOValue(data.lvlUpCurrentValue, sliderFillDuration);
                    });

                }
                else
                {
                    SetLvlTxtAnimation();
                    slider.DOValue(slider.maxValue, sliderFillDuration);
                }
            }
            else
            {
                slider.DOValue(data.lvlUpCurrentValue, sliderFillDuration);
            }

            data.SaveData();

        }
        else
        {
            SetLvlTxtAnimation();
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

            MakeObjectScale(star, starCloneDefaultScale + .4f, starCloneDefaultScale / 2, starsDuration / 2);

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

        if (!revertPos)
        {
            midPath.x += Random.Range(0, midOffset);
        }
        else
            midPath.x -= Random.Range(0, midOffset);

        Vector3[] path = { clone.position, midPath, destination };
        clone.DOPath(path, starsDuration, PathType.CatmullRom, PathMode.Sidescroller2D).OnComplete(() =>
        {
            completeAction();
            clone.gameObject.SetActive(false);
        });
    }

    void MakeStarIconScale()
        => MakeObjectScale(starIcon, starIconDefaultScale + .4f, starIconDefaultScale, .2f);

    void MakeObjectScale(Transform t, float targetScale, float originScale, float duration) =>
        t.DOScale(targetScale, duration).OnComplete(() => { t.DOScale(originScale, duration); });

    Vector2 GetScreenPos(Vector3 worldPos) => RectTransformUtility.WorldToScreenPoint(cam, worldPos);

    void SetSliderMaxValue() => slider.maxValue = data.maxLvlUpUnit[data.lvlUpCounter].maxLvlValue;

    void OnLocalizedTextChanged(string localizedValue)
    {
        SetLevelText();
    }

    void SetLvlTxtAnimation()
    {
        SetLevelText();
        lvlTxt.transform.DOScale(lvlTxt.transform.localScale + new Vector3(.2f, 0, 0), .3f).OnComplete(
            () => { lvlTxt.transform.DOScale(lvlTxt.transform.localScale - new Vector3(.2f, 0, 0), .3f); });
    }

    void SetLevelText() 
    {
        if (LvlIsMax())
        {
            localizeStringEvent.StringReference.TableEntryReference = maxLvlKey;
            lvlTxt.text = localizeStringEvent.StringReference.GetLocalizedString();
        }
        else
        {
            localizeStringEvent.StringReference.TableEntryReference = lvlKey;
            currentLevel = data.lvlUpCounter + 1;
            lvlTxt.text = string.Concat(localizeStringEvent.StringReference.GetLocalizedString(), " ", currentLevel);
        }

    }

    bool LvlIsMax() => data.lvlUpCounter + 1 > data.maxLvlUpUnit.Count;

}
