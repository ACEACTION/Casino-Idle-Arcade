using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{
    public static ChipDesk instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] float cooldown;
    public float cooldownAmount;
    public Customer customer;
    public Transform customerSpot;
}
