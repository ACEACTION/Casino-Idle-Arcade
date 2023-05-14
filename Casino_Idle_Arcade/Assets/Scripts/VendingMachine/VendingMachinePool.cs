using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class VendingMachinePool : MonoBehaviour
{
    [SerializeField] Snack[] snackPrefab;

    public ObjectPool<Snack> snackPool;

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
        snackPool = new ObjectPool<Snack>(
            CreateSnack, OnGet, OnRelease, OnDestroySnack, false, 100, 100000);
    }
    private void OnDestroySnack(Snack obj)
    {
        Destroy(obj);
    }

    private void OnRelease(Snack obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(Snack obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Snack CreateSnack()
    {
        Snack snack =
            Instantiate(snackPrefab[Random.Range(0,2)], transform.position, Quaternion.identity);
        return snack;
    }

    public void OnReleaseSnack(Snack snack)
    {
        snackPool.Release(snack);
    }
}
