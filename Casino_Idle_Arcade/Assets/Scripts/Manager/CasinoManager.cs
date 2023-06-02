using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementsType
{
    roulette, jackpot, baccarat, VendingMachine, bar
}
public class CasinoManager : MonoBehaviour
{
    public int defaultMoney;
    public bool isCompleteTutorial;
    [SerializeField] GameObject tutorialManager;
    public List<ElementsType> availableElements = new List<ElementsType>();
    public static CasinoManager instance;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }



        if (isCompleteTutorial)
        {
            GameManager.isCompleteTutorial = true;
            tutorialManager.SetActive(false);
            GameManager.AddMoney(defaultMoney);
        }
        else
            tutorialManager.SetActive(true);
    }

   

}
