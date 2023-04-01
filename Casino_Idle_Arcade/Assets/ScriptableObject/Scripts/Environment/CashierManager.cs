using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;
public class CashierManager : MonoBehaviour
{
    [SerializeField] int slotMachinePayment;
    [SerializeField] int tablePayment;
    [SerializeField] WorkerCheker workerCheker;
    [SerializeField] Slider slider;



    public List<Transform> customerSpots = new List<Transform>();

    [SerializeField] float cooldown;
    [SerializeField] float cooldownAmount;

    [SerializeField] CashierFirstTransform firstCounter;
    [SerializeField] StackMoney stackMoney;
    public bool cashierAvailabe;
    public bool playerIsCashier;
    public int tableIndex = 0;
    Table emptyTable;
    private void Update()
    {
        //slider.minValue = -cooldown;
        slider.value = -cooldown;
        //if cashier spot !null
        if ((workerCheker.isPlayerAvailable || workerCheker.isWorkerAvailable)
            && firstCounter.firstCustomer != null)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                if (CasinoElementManager.SendCustomerToElement(firstCounter.firstCustomer))
                {
                    cooldown = cooldownAmount;
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
