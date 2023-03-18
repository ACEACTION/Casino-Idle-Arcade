using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] CashierFirstTransform firstCounter;
    public bool cashierAvailabe;
    public bool playerIsCashier;
    private void Update()
    {
        //if cashier spot !null
        if ((playerIsCashier || cashierAvailabe)
            && firstCounter.firstCustomer != null)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                if(SlotMachineManager.emtpySlotMachines.Count != 0)
                {
                    firstCounter.firstCustomer.SetMove(SlotMachineManager.emtpySlotMachines[0].customerSpot);
                    SlotMachineManager.emtpySlotMachines[0].isEmpty = false;
                    SlotMachineManager.emtpySlotMachines[0].customer = firstCounter.firstCustomer;
                    SlotMachineManager.emtpySlotMachines.Remove(SlotMachineManager.emtpySlotMachines[0]);
                    cooldown = 3f;

                }
            }
        
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerIsCashier = true;
        }
        if (other.gameObject.CompareTag("Cashier"))
        {
            cashierAvailabe = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsCashier = false;

        }
    }

}
