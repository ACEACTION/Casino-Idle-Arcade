using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] CustomerMovement customer;
    public ObjectPool<CustomerMovement> customerPool;

    public static CustomerPool instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        customerPool = new ObjectPool<CustomerMovement>(CreateCustomer, OnGet, OnRelease, OnDestroyCustomer, false, 10, 50);
    }

    private void OnDestroyCustomer(CustomerMovement obj)
    {
        Destroy(obj);
    }

    private void OnRelease(CustomerMovement obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(CustomerMovement obj)
    {
        obj.gameObject.SetActive(true);
    }

    private CustomerMovement CreateCustomer()
    {
        CustomerMovement cs = Instantiate(customer);

        return cs;
    }

    public void OnReleaseHit(CustomerMovement customer)
    {
        customerPool.Release(customer);
    }
}
