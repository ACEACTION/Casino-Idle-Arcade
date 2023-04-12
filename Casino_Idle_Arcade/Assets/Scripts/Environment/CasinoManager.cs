using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementsType
{
    roulette, jackpot, bar
}
public class CasinoManager : MonoBehaviour
{
    public static CasinoManager instance;
    public List<ElementsType> availableElements = new List<ElementsType>();

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }

        GameManager.totalMoney += 900;
    }

}
