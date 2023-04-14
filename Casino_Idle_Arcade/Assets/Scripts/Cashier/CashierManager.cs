using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CashierManager : MonoBehaviour
{


    public bool cashierAvailabe;
    public bool playerIsCashier;
    public int tableIndex = 0;
    [SerializeField] float cooldown;

    public CashierData data;
    public List<Transform> customerSpots = new List<Transform>();
    public WorkerCheker workerCheker;
    [SerializeField] Slider slider;
    [SerializeField] Image firstCustomerGameIcon;
    [SerializeField] CashierFirstTransform firstCounter;
    [SerializeField] StackMoney stackMoney;

    private void Start()
    {
        cooldown = data.cooldownAmount;
        slider.maxValue = cooldown;
    }

    private void Update()
    {

        //if (player != null && cooldown == 0)
        //{
        //    timePassed += Time.deltaTime;
        //    if (timePassed >= maxTimePassed)
        //    {

        //    }
        //}
        //else if (cooldown > 0)
        //{
        //    cooldown -= Time.deltaTime;            
        //}

        //if cashier spot !null
        if ((workerCheker.isPlayerAvailable || workerCheker.isDealerAvailabe)
            && firstCounter.nextCustomer)
        {
            cooldown -= Time.deltaTime;
            slider.value += Time.deltaTime;

            firstCustomerGameIcon.gameObject.SetActive(true);
            firstCustomerGameIcon.sprite = 
                data.GetCasinoGameIcon(firstCounter.firstCustomer.elementType);
            if (cooldown <= 0)
            {
                if (CasinoElementManager.SendCustomerToElement(firstCounter.firstCustomer))
                {
                    firstCounter.firstCustomer.PayMoney(stackMoney, 
                        data.GetPayment(firstCounter.firstCustomer.elementType));
                    cooldown = data.cooldownAmount;
                    slider.value = 0;
                    firstCounter.nextCustomer = false;
                    firstCustomerGameIcon.gameObject.SetActive(false);
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
