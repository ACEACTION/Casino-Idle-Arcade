using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementsType
{
    roulette, jackpot, bar
}
public class CasinoManager : MonoBehaviour
{
    [SerializeField] bool isCompleteTutorial;
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

        GameManager.totalMoney += 900;

        if (isCompleteTutorial)
        {
            GameManager.isCompleteTutorial = true;
            tutorialManager.SetActive(false);
        }
        else
            tutorialManager.SetActive(true);
    }

}
