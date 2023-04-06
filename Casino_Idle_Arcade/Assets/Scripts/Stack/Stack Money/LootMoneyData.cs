using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Loot Money Data")]
public class LootMoneyData : ScriptableObject
{
    public float lootMinYOffset;
    public float lootMaxYOffset;
    public float lootXOffset;
    public float lootZOffset;
    public float lootMoveUpDuration;
    public float lootRotDuration;
    public float lootScaleDuration;
}
