using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_StartMoney : MonoBehaviour
{
    [SerializeField] StackMoney stackMoney;
    void Start()
    {
        if (!CasinoManager.instance.isCompleteTutorial)
        {
            stackMoney.gameObject.SetActive(true);
            stackMoney.totalMoney = CasinoManager.instance.defaultMoney;
        }
        else
            stackMoney.gameObject.SetActive(false);


    }

}
