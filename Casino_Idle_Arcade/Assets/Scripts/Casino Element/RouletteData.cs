using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/Roulette Data")]
public class RouletteData : ScriptableObject
{
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
