using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] float castTime;
    public float castTimeAmount;

    public Animator dealerAnim;
    [SerializeField] float playingTime;
    public float playingTimeAmount;

    public int customersInPlaceCount = 0;
    public List<CustomerMovement> customersInPlace = new List<CustomerMovement>(6);
    public List<CustomerMovement> customerList = new List<CustomerMovement>();
    public List<Transform> customerSpot = new List<Transform>();
    public int maximumCapacity;
    public bool hasEmptySlots;
    public bool readyToPlay;

    public bool customersPlaying = true;
    public bool playerIsDealer;
    public bool dealerAvailabe;


    public Customer winnerCustomer;
    private void Start()
    {
        playingTime = playingTimeAmount;
        castTime = castTimeAmount;
/*        GameInstrumentsManager.tableList.Add(this);
        GameInstrumentsManager.emptyTableList.Add(this);*/
    }

    private void Update()
    {
        if (readyToPlay && (playerIsDealer || dealerAvailabe))
        {
            dealerAnim.SetBool("playingCard", true);
            castTime -= Time.deltaTime;
            if(castTime <= 0 && customersPlaying)
            {

                //disabling dealer animation here
                dealerAnim.SetBool("playingCard", false);

                ///playing starts here

                //setting customers animation to playing mode
                foreach (Customer cost in customerList)
                {
                    cost.anim.SetBool("isPlayingCard", true);
                }
                //adding chipEffect to tables
                //ChipSpawning();
                playingTime -= Time.deltaTime;
                if(playingTime <= 0 && customersPlaying)
                {
                    foreach (Customer cost in customerList)
                    {
                        cost.anim.SetBool("isPlayingCard", false);
                    }
                    customersPlaying = false;
                    //pickingWinner
                    winnerCustomer = customerList[Random.Range(0, customerList.Count)];
                    winnerCustomer.tableWinner = true;


                    winnerCustomer.anim.SetBool("isWinning", true);
                    StartCoroutine(WaitingForWinner());

                }
            }
        }

        if(customerList.Count == maximumCapacity)
        {
            hasEmptySlots = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsDealer = true;
        }
        if (other.gameObject.CompareTag("Dealer"))
        {
            dealerAvailabe = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsDealer = false;

        }
    }

    IEnumerator WaitingForWinner()
    {
        dealerAnim.SetBool("playingCard", false);

        yield return new WaitForSeconds(3f);
        foreach(CustomerMovement cs in customerList)
        {
            cs.Leave();
        }
        castTime = castTimeAmount;
        playingTime = playingTimeAmount;
        customersPlaying = true;
       // GameInstrumentsManager.emptyTableList.Add(this);

    }

    void ChipSpawning()
    {

    }
}
