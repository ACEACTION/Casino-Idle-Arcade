using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrade_Item_Btn : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI itemLvlTxt;
    [SerializeField] TextMeshProUGUI itemBtnTxt;
    [SerializeField] GameObject cashIcon;
    [SerializeField] Image defaultBg;
    [SerializeField] Image maxedBg;
    [SerializeField] GameObject maxedIcon;
    [SerializeField] GameObject maxedTxt;
    [SerializeField] Button btn;

    public void InitItemTxt(string lvlTxt, int itemPrice)
    {
        itemLvlTxt.text = lvlTxt;
        itemBtnTxt.text = itemPrice.ToString();

    }

    public void SetMaxedState(bool canUpgrade)
    {
        /* if item was maxed, canUpgrade was false so we had to deactive default state and 
            active max state */
        defaultBg.gameObject.SetActive(canUpgrade);
        cashIcon.SetActive(canUpgrade);
        itemBtnTxt.gameObject.SetActive(canUpgrade);
        maxedBg.gameObject.SetActive(!canUpgrade);
        maxedIcon.SetActive(!canUpgrade);
        maxedTxt.SetActive(!canUpgrade);

        if (canUpgrade)
            btn.targetGraphic = defaultBg;
        else
            btn.targetGraphic = maxedBg;
    }

    

}
