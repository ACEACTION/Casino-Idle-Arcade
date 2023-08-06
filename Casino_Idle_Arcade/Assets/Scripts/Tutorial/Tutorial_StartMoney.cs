using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_StartMoney : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] StackMoney stackMoney;
    void Start()
    {
        //if (!CasinoManager.instance.isCompleteTutorial)
        if (!GameManager.isCompleteTutorial)
        {
            stackMoney.gameObject.SetActive(true);
            //stackMoney.totalMoney = CasinoManager.instance.defaultMoney;
            stackMoney.totalMoney = tutorialManager.startMoney;
        }
        else
            stackMoney.gameObject.SetActive(false);


    }

}
