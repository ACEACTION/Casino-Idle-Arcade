using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NormalCustomer data")]
public class CustomerData : ScriptableObject
{
    [SerializeField] float moveSpeed;
    [SerializeField] int cashAmount;
    
}
