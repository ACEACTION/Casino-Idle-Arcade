using UnityEngine;
using UnityEngine.Pool;

public class StackMoneyMaker : MonoBehaviour
{
    [SerializeField] Money moneyPrefab;
    public ObjectPool<Money> pool;

    public static StackMoneyMaker Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    void Start()
    {
        pool = new ObjectPool<Money>(
            CreateMoney, OnGet, OnRelease, OnDestoryMoney, false, 100, 100000);
    }

    private void OnDestoryMoney(Money obj)
    {
        Destroy(obj);
    }

    private void OnRelease(Money obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGet(Money obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Money CreateMoney()
    {
        Money money = 
            Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        return money;
    }

    public void OnReleaseMoney(Money money)
    {
        pool.Release(money);
    }


}
