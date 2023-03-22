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
        if(instance == null)
        {
            instance = this;
        }
    }
}
