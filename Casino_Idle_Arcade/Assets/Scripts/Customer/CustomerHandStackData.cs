using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Customer Hand Stack Data")]
public class CustomerHandStackData : ScriptableObject
{
    public float stackYOffset;
    public float removeChipDelay;
    public float removeChipToDeskTime;
    public float moneyFromDeskDuration;
    public Vector3 firstStackPos;
}
