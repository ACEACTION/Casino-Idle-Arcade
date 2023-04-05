using UnityEngine;
using UnityEngine.Pool;


public class StackMoneyPool : MonoBehaviour
{
    [SerializeField] Money moneyPrefab;
    public ObjectPool<Money> pool;

    public static StackMoneyPool Instance;
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
        //obj.transform.DOScale(moneyPrefab.transform.localScale, 0);
        //obj.transform.localScale = moneyPrefab.transform.localScale;
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
