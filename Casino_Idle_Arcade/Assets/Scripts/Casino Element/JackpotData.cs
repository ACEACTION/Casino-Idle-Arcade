using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Jackpot Data")]
public class JackpotData : ScriptableObject
{
    [Range(0, 1)]
    public float[] winProbabilityUpgradeLevel;
    public int[] moneyAmountUpgradeLevel;
    public float[] playGameTime;
    public float afterGameDuration;
}
