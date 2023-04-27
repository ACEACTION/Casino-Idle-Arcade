using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Casino Game Stack Data")]
public class CasinoGameStackData : ScriptableObject
{
    public int[] maxStackCountLevel;
    public float stackYOffset;
    public float addResourceToStackTime;
    [HideInInspector] public float iconDefaultScale;
    [HideInInspector] public float stackDefaultScale;

}
