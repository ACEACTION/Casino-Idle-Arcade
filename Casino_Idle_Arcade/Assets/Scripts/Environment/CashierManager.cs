using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    [SerializeField] int slotMachinePayment;
    [SerializeField] int tablePayment;


    public List<Transform> customerSpots = new List<Transform>();

    [SerializeField] float cooldown;
    [SerializeField] CashierFirstTransform firstCounter;
    [SerializeField] StackMoney stackMoney;
    public bool cashierAvailabe;
    public bool playerIsCashier;
    public int tableIndex = 0;
    Table emptyTable;
    private void Update()
    {
        //if cashier spot !null
        if ((playerIsCashier || cashierAvailabe)
            && firstCounter.firstCustomer != null)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                if(GameInstrumentsManager.emtpySlotMachines.Count != 0)
                {
                    print("emptyyyy");
                    firstCounter.firstCustomer.PayMoney(stackMoney, slotMachinePayment);
                    //sending customers to slot machines
                    firstCounter.firstCustomer.SetMove(GameInstrumentsManager.emtpySlotMachines[0].customerSpot);
                    GameInstrumentsManager.emtpySlotMachines[0].isEmpty = false;
                    GameInstrumentsManager.emtpySlotMachines[0].customer = firstCounter.firstCustomer;
                    GameInstrumentsManager.emtpySlotMachines.Remove(GameInstrumentsManager.emtpySlotMachines[0]);

                    cooldown = 2f;
                }
                else if(GameInstrumentsManager.emptyTableList.Count != 0)
                {

                    emptyTable = GameInstrumentsManager.emptyTableList[0];

                    //sending customers to tables
                    if (emptyTable.hasEmptySlots)
                    {
                        firstCounter.firstCustomer.PayMoney(stackMoney, tablePayment);

                        firstCounter.firstCustomer.SetMove(emptyTable.customerSpot[tableIndex]);
                        emptyTable.customerList.Add(firstCounter.firstCustomer);
                        emptyTable.customersInPlace[tableIndex] = firstCounter.firstCustomer;
                        cooldown = 2f;
                        tableIndex++;

                        if(tableIndex  >= emptyTable.maximumCapacity)
                        {
                            GameInstrumentsManager.emptyTableList.Remove(emptyTable);
                            tableIndex = 0;
                        }
                    }

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
