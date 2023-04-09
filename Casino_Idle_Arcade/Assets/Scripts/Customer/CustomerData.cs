using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CustomerTarget
{
    roulette, jackpot,bar
}

[CreateAssetMenu(menuName = "Data/ Customer Data")]
public class CustomerData : ScriptableObject
{
    [SerializeField] float moveSpeed;    
    
}
