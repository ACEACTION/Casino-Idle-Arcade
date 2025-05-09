using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackMoney : MonoBehaviour
{
    public event System.Action StackCounterOnChanged;
    int stackCounter;
    public int StackCounter
    {
        get { return stackCounter; }
        set
        {
            if (stackCounter != value)
            {
                stackCounter = value;
                StackCounterOnChanged?.Invoke();
            } 
        }
    }

    public int a;
    private void Update()
    {
        a = StackCounter;
    }

    [Header("Stack")]    
    [HideInInspector] public bool isPlayer;
    public int totalMoney;

    [Header("References")]
    [SerializeField] StackMoneyData stackData;
    [SerializeField] LootMoneyData lootData;
    [SerializeField] StackMoneySlot prefab; // the prefab to create
    [SerializeField] List<StackMoneySlot> slots;
    [SerializeField] List<Money> moneyList;
    [SerializeField] StackMoneyCanvas stackMoneyCanvas;

    private void Start()
    {
        MakeSlots();
    }

    void MakeSlots()
    {
        for (int x = 0; x < stackData.xSize; x++)
        {
            for (int y = 0; y < stackData.ySize; y++)
            {
                for (int z = 0; z < stackData.zSize; z++)
                {
                    StackMoneySlot slot =
                        Instantiate(prefab,
                        transform.position + new Vector3
                        (y * stackData.xOffset, x * stackData.yOffset, z * stackData.zOffset),
                        Quaternion.identity);
                    slot.transform.SetParent(transform);
                    slots.Add(slot);
                }
            }
        }
    }

    public void InitStackMoney(int moneyAmout, MoneyType type)
    {
        for (int i = 0; i < moneyAmout; i++)
        {         
            Money money = StackMoneyPool.Instance.pool.Get();
            money.transform.position = transform.position;
            money.transform.eulerAngles = new Vector3(0, 0, 0);
            money.SetMoneyAmount(type);
            AddToStack(money);
        }
    }


    //private void Update()
    //{
    //    if (isPlayer)
    //    {
    //        foreach (Money money in moneyList)
    //        {
    //            money.transform.DOMove(money.transform.position + GetRandomLootOffset(), lootData.lootMoveUpDuration)
    //                .OnComplete(() =>
    //                {
    //                    money.SetGoToPlayer();
    //                });

    //            money.transform.DORotate(new Vector3(Random.Range(0, 360),
    //                Random.Range(0, 360),
    //                Random.Range(0, 360)), lootData.lootRotDuration);

    //            money.transform.DOScale(.4f, lootData.lootScaleDuration);
    //        }
    //        stackCounter = 0;
    //        moneyList.Clear();
    //    }
    //}
   

    void MoveUpMoneyAndGoToPlayer()
    {
        foreach (Money money in moneyList)
        {
            money.transform.DOMove(money.transform.position + GetRandomLootOffset(), lootData.lootMoveUpDuration)
                .OnComplete(() =>
                {
                    money.SetGoToPlayer();
                });

            money.transform.DORotate(new Vector3(Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)), lootData.lootRotDuration);

            money.transform.DOScale(.4f, lootData.lootScaleDuration);
        }

        StackCounter = 0;
        moneyList.Clear();
    }

    public void AddToStack(Money money)
    {

        totalMoney += money.moneyAmount;
        money.transform.eulerAngles = new Vector3(0, Random.Range(90, 560), 0);

        if (!isPlayer)
        {
            if (StackCounter < slots.Count)
            {                
                SetMoneyParent(money.transform, slots[StackCounter].transform);
                money.transform.DOLocalJump(Vector3.zero, stackData.jumpPower, 1, stackData.moneyMoveDuration)
                    .SetEase(stackData.moneyMoveEase);
                moneyList.Add(money);
            }
            else
            {                
                SetMoneyParent(money.transform, slots[slots.Count - 1].transform);
                money.transform.DOLocalJump(Vector3.zero, stackData.jumpPower, 1, stackData.moneyMoveDuration)
                    .SetEase(stackData.moneyMoveEase)
                    .OnComplete(() => money.ReleaseResource());
            }

            money.transform.DORotate(new Vector3(0, 0, 0), stackData.GetRotationDuration(), RotateMode.FastBeyond360)
                .OnComplete(() =>
                {
                    money.transform.DORotate(new Vector3(0, Random.Range(-7, 7), 0), .5f, RotateMode.FastBeyond360);

                });
            StackCounter++;

        }
        else
        {            
            GiveMoneyToPlayerProcess();
            money.SetGoToPlayer();
        }
    }

                
    void SetMoneyParent(Transform money, Transform parent) 
        => money.SetParent(parent);



    Vector3 GetRandomLootOffset()
    {
        return new Vector3
            (Random.Range(-lootData.lootXOffset, lootData.lootXOffset),
            Random.Range(lootData.lootMinYOffset, lootData.lootMaxYOffset),
            Random.Range(-lootData.lootZOffset, lootData.lootZOffset));
    }


    public void GetFromStack()
    {
        StackCounter = 0;        
        StartCoroutine(GoToPlayer());
    }

    IEnumerator GoToPlayer()
    {
        for (int i = 0; i < moneyList.Count; i++)
        {
            yield return new WaitForSeconds(stackData.goToPlayerDelay);
            moneyList[i].SetGoToPlayer();
        }
        
        
        moneyList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;


            if (totalMoney > 0)
            {
                //stackMoneyCanvas.AddMoneyText(totalMoney);
                MoveUpMoneyAndGoToPlayer();
                GiveMoneyToPlayerProcess();
            }
        }
    }

    void GiveMoneyToPlayerProcess()
    {
        AudioSourceManager.Instance.PlayCashPickupSfx();
        GameManager.AddMoney(totalMoney);
        LootMoneu_UI.Instance?.ShowLootMoneyTxt(totalMoney);
        Money_UI.Instance?.SetTotalMoneyTxt();
        totalMoney = 0;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            isPlayer = false;
        }
    }

}
