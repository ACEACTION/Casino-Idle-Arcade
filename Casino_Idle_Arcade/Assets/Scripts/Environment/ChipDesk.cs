using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{

    private void Start()
    {
        ChipDeskManager.chipDeskList.Add(this);
    }


    [SerializeField] float cooldown;
    public float cooldownAmount;
    public Customer customer;
    public Transform customerSpot;
}
