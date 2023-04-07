using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{

    private void Start()
    {
        ChipDeskManager.chipDeskList.Add(this);
    }


    [SerializeField] float cooldown;
    public float cooldownAmount;
    public Customer customer;
    public Transform customerSpot;
    public Transform chipPoint;
    public Transform moneySpawnPoint;

    public Money GiveMoney()
    {
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = moneySpawnPoint.position;
        return money;
    }

}
