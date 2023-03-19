using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] float cooldown;
    public float cooldownAmount;
    public Customer customer;
    public Transform customerSpot;
    [SerializeField] float winPorbability;
    public bool isEmpty = true;
    private bool customerIsPlaying;
    private void Start()
    {
        cooldown = cooldownAmount;
        GameInstrumentsManager.slotMachineList.Add(this);
        GameInstrumentsManager.emtpySlotMachines.Add(this);
    }

    private void Update()
    {
        if (customerIsPlaying)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                //customer should leave the machine here
                customer.Leave();
            }
        
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (customer != null)
        {
            if (other.gameObject.Equals(customer.gameObject))
            {
                customerIsPlaying = true;
                customer.anim.SetBool("idle", false);
                customer.anim.SetBool("isPlayingCard", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (customer != null)
        {
            if (other.gameObject.Equals(customer.gameObject))
            {
                cooldown = cooldownAmount;
                customerIsPlaying = false;
                isEmpty = true;
                GameInstrumentsManager.emtpySlotMachines.Add(this);
            }
        }
    }
}
