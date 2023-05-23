using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Casino Game Stack Data")]
public class ElementStackData : ScriptableObject
{
    public int[] maxStackCountLevel;
    public float stackYOffset;
    public float jumpDuration;
    public float scaleDuration;
    public float jumpPower;
    [HideInInspector] public float iconDefaultScale;
    [HideInInspector] public float stackDefaultScale;

}
