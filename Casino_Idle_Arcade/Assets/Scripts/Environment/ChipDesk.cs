using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{
    [SerializeField] float cooldown;
    public float cooldownAmount;
    public Customer customer;
    public Transform customerSpot;
}
