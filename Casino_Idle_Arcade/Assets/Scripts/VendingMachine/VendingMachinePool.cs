using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class VendingMachinePool : MonoBehaviour
{
    [SerializeField] CasinoFood foodPrefab;

    public ObjectPool<CasinoFood> casinoFoodPool;

    public static VendingMachinePool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        casinoFoodPool = new ObjectPool<CasinoFood>(
            CreateSnack, OnGet, OnRelease, OnDestroySnack, false, 100, 100000);
    }
    private void OnDestroySnack(CasinoFood obj)
    {
        Destroy(obj);
    }

    private void OnRelease(CasinoFood obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(CasinoFood obj)
    {
        obj.gameObject.SetActive(true);
    }

    private CasinoFood CreateSnack()
    {
        CasinoFood snack =
            Instantiate(foodPrefab, transform.position, Quaternion.identity);
        return snack;
    }

    public void OnReleaseSnack(CasinoFood snack)
    {
        casinoFoodPool.Release(snack);
    }
}
