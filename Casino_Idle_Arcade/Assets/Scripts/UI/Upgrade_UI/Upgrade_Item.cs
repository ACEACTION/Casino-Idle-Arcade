using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Upgrade_Item : MonoBehaviour
{    
    public Image item_bg;
    public Button item_btn;
    [SerializeField] TextMeshProUGUI itemCost;
    [SerializeField] GameObject cashIcon;
    [SerializeField] GameObject maxTxt;

    public void SetDisableItemBtnColor(Color disableBgColor)
    {
        item_bg.color = disableBgColor;
    }

    public void SetItemCost(int cost) => itemCost.text = cost.ToString();

    public void SetMaxLevelItem()
    {
        itemCost.gameObject.SetActive(false);
        cashIcon.SetActive(false);
        maxTxt.SetActive(true);
    }
}
