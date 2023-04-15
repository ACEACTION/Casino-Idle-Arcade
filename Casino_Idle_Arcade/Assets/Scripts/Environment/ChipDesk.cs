using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{
    public Customer customer;
    public Transform customerSpot;
    public Transform chipPoint;
    public Transform moneySpawnPoint;
    public Transform chipSpawnPoint;
    public StackMoney stackMoney;

    private void Start()
    {
        ChipDeskManager.chipDeskList.Add(this);
    }

    public Money GiveMoney()
    {
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = moneySpawnPoint.position;
        return money;
    }



}
