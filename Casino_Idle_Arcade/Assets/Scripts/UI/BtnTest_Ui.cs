using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTest_Ui : MonoBehaviour
{
    public int extraMoney;
    public int maxStack;

    public void AddCash()
    {
        GameManager.AddMoney(extraMoney);
    }

    public void AddStack()
    {
        PlayerMovements.Instance.handStack.AddMaxStackCounter(2);
        if (PlayerMovements.Instance.handStack.data.maxStackCount > maxStack) PlayerMovements.Instance.handStack.data.maxStackCount = maxStack;
    }

}
