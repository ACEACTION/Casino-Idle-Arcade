using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Roulette Data")]
public class RouletteData : ScriptableObject
{
    [Tooltip("the games must played to reach a heavy bet")]
    [SerializeField] int minGamesCounterForHeavyBet;
    [SerializeField] int maxGamesCounterForHeavyBet;
    [HideInInspector] public int gamesCounterForHeavyBet 
    { get { return Random.Range(minGamesCounterForHeavyBet, maxGamesCounterForHeavyBet); } }
    public int heavyChipAmount;
    
    [Tooltip("chips amount for diff table upgrade lvl")]
    public int[] chipAmountPerLevel;

    public int heavyMoneyAmount;
    public int[] moneyAmountLevel;    

    [SerializeField] float[] maxPlayCdLevel;
    public float getChipDuration;
    public float cleaningCdAmount;
    public int betUnitPrice;
    public float maxGetChipCd;
    public float playChipSpotOffset;
    public Vector3 playChipSpotDefaultPos;
    public float afterGameDuration = 2f;


    public float GetMaxPlayCdLevel(int upgradeIndex) => maxPlayCdLevel[upgradeIndex];
}
