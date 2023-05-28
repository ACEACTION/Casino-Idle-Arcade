using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrade_Item_Btn : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI itemLvlTxt;
    [SerializeField] TextMeshProUGUI itemBtnTxt;
    [SerializeField] GameObject cashIcon;
    [SerializeField] GameObject defaultBg;
    [SerializeField] GameObject maxedBg;
    [SerializeField] GameObject maxedIcon;
    [SerializeField] GameObject maxedTxt;

    
    public void InitItemTxt(string lvlTxt, int itemPrice)
    {
        itemLvlTxt.text = lvlTxt;
        itemBtnTxt.text = itemPrice.ToString();
    }

    public void SetMaxedState(bool canUpgrade)
    {
        /* if item was maxed, canUpgrade was false so we had to deactive default state and 
            active max state */
        defaultBg.SetActive(canUpgrade);
        cashIcon.SetActive(canUpgrade);
        itemBtnTxt.gameObject.SetActive(canUpgrade);

        maxedBg.SetActive(!canUpgrade);
        maxedIcon.SetActive(!canUpgrade);
        maxedTxt.SetActive(!canUpgrade);
    }

    

}
