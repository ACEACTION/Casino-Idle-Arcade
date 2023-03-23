using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour
{
    public ElementsType elementType;
    public Animator anim;
    public CustomerData stats;
    bool payedMoney;
    public bool tableWinner;


    private void OnEnable()
    {
        SetElementType();
    }


    void SetElementType()
    {
        elementType = CasinoManager.instance.availableElements[Random.Range(0, CasinoManager.instance.availableElements.Count)];

    }
    public void PayMoney(StackMoney stackMoney, int amount)
    {
        if (payedMoney) return;

        for (int i = 0; i < amount; i++)
        {
            Money money = StackMoneyPool.Instance.pool.Get();
            money.transform.position = transform.position;
            stackMoney.AddToStack(money);
        }
    }

}
