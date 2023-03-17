using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] GameObject customer;
    public ObjectPool<GameObject> customerPool;

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
        customerPool = new ObjectPool<GameObject>(CreateCustomer, OnGet, OnRelease, OnDestroyCustomer, false, 10, 50);
    }

    private void OnDestroyCustomer(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private GameObject CreateCustomer()
    {
        GameObject cs = Instantiate(customer);

        return cs;
    }

    public void OnReleaseHit(GameObject customer)
    {
        customerPool.Release(customer);
    }
}
