using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] float cooldown;
    public float cooldownAmount;
    public List<Customer> customerList = new List<Customer>();
    public List<Transform> customerSpot = new List<Transform>();
    public int maximumCapacity;
    public bool hasEmptySlots;

    private void Start()
    {
        cooldown = cooldownAmount;
        GameInstrumentsManager.tableList.Add(this);
        GameInstrumentsManager.emptyTableList.Add(this);
    }

    private void Update()
    {
        if(customerList.Count == maximumCapacity)
        {
            hasEmptySlots = false;
        }
    }
}
