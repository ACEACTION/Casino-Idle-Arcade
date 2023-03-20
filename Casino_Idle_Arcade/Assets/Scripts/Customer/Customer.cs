using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    inLine, firstCounter, movingIn, leaving, playingGame
}
public class Customer : MonoBehaviour
{    
    public Animator anim;
    public CustomerState csState;
    public CustomerData stats;
    bool payedMoney;
    public bool tableWinner;


    public void PayMoney(StackMoney stackMoney, int amount)
    {
        if (payedMoney) return;

        for (int i = 0; i < amount; i++)
        {
            Money money = StackMoneyMaker.Instance.pool.Get();
            money.transform.position = transform.position;
            stackMoney.AddToStack(money);
        }
    }

}
