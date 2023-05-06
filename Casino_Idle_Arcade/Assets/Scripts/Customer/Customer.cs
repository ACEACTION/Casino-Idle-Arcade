using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour
{
    //public ElementsType elementType;
    public Animator anim;
    public CustomerData stats;
    public ParticleSystem[] confetti;
    public bool isWinning = false;
    public bool tableWinner;
    public bool isLeaving;
    public ParticleSystem[] happyEmojies, sadEmojies;
    [SerializeField] int moneyAmount;
    public CustomerHandStack stack;
    public ChipDesk chipDesk = null;
    [SerializeField] GameObject[] customerCards;


    private void OnEnable()
    {      
    }

    public void SetPlayingCardAnimation(bool state)
    {
        anim.SetBool("isPlayingCard", state);
    }
    public void SetJackPotPlayingAnim(bool state)
    {
        anim.SetBool("isPlayingJackPot", state);
    }

    public void SetPlayingJackPotAnimation(bool state)
    {
        anim.SetBool("isPlayingJackPot", state);
    }

    public void SetWinningAnimation(bool state)
    {
        anim.SetBool("isWinning", state);
    }
    public void SetSadWalkAnimation(bool state)
    {
        anim.SetBool("sadWalking", state);
    }

    public void SetLosingAnimation(bool state)
    {
        anim.SetBool("isLosing", state);
    }


    public void PayMoney(StackMoney stackMoney, int amount, MoneyType type)
    {
        
        for (int i = 0; i < amount; i++)
        {
           // yield return new WaitForSeconds(0f);
            Money money = StackMoneyPool.Instance.pool.Get();
            money.transform.position = transform.position;
            money.transform.eulerAngles = new Vector3(0, 0, 0);
            money.SetMoneyAmount(type);
            stackMoney.AddToStack(money);
        }

    }

    
    public int Bet(int betUnitPrice)
    {
        int betAmount = Random.Range(betUnitPrice / 100, moneyAmount / 100);
        return betAmount * 100;
    }

    public void SetChipDesk() => chipDesk = ChipDeskManager.FindNearestChipDesk(transform);

    private void OnTriggerEnter(Collider other)
    {
        // customer go to get cash from chips
        if (chipDesk && other.gameObject .Equals (chipDesk.gameObject))
        {
            stack.RemoveChipsFromStack(chipDesk);
        }
    }


    public void SetCustomerCardsActiveState(bool state)
    {
        foreach (GameObject card in customerCards)
        {
            card.gameObject.SetActive(state);
        }
    }

    public Vector3 GetCustomerCardPos() => customerCards[1].transform.position;

}
