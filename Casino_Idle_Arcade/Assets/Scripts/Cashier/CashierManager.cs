using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CashierManager : MonoBehaviour
{
    public bool cashierAvailabe;
    public bool playerIsCashier;
    public int tableIndex = 0;
    public int maxCapacity = 5;
    [SerializeField] float cooldown;
    public List<CustomerMovement> customersList = new List<CustomerMovement>();

    public CashierData data;
    public List<Transform> customerSpots = new List<Transform>();
    public EmployeeCheker workerCheker;
    [SerializeField] Slider slider;
    [SerializeField] Image firstCustomerGameIcon;
    [SerializeField] CashierFirstTransform firstCounter;
    [SerializeField] StackMoney stackMoney;

    public CasinoElement casinoElement;    

    private void Start()
    {
        cooldown = data.cooldownAmount;
        slider.maxValue = cooldown;
        CustomerSpawner.instance.maxCustomer += 5;
        slider.gameObject.SetActive(false);
    }

    private void Update()
    {

        if ((workerCheker.isPlayerAvailable || workerCheker.isDealerAvailabe)
            && firstCounter.nextCustomer)
        {            

            casinoElement = CasinoElementManager.CanSendCustomerToElement();
            if (casinoElement)
            {
                slider.gameObject?.SetActive(true);
                firstCustomerGameIcon.gameObject.SetActive(true);
                //firstCustomerGameIcon.sprite = 
                //       data.GetCasinoGameIcon(casinoElement.elementType);

                firstCustomerGameIcon.sprite = data.GetIcon(casinoElement.elementType);


                cooldown -= Time.deltaTime;
                slider.value += Time.deltaTime;
                if (cooldown <= 0)
                {
                    firstCounter.firstCustomer.PayMoney(stackMoney,
                            data.GetPayment(casinoElement.elementType),
                            MoneyType.receptionMoney);
                                       


                    casinoElement.SendCustomerToElement(firstCounter.firstCustomer);
                    casinoElement = null;
                    cooldown = data.cooldownAmount;
                    slider.value = 0;
                    firstCounter.nextCustomer = false;
                    firstCustomerGameIcon.gameObject.SetActive(false);
                    slider.gameObject.SetActive(false);
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
