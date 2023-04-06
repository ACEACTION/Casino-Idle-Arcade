using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JackpotMoneyStack : MonoBehaviour
{
    [SerializeField] float dropMoneyOffset;
    bool isPlayer;

    [SerializeField] LootMoneyData data;
    [SerializeField] JackPot jackpot;
    [SerializeField] List<Money> moneyList = new List<Money>();


    public void MakeMoney()
    {
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = jackpot.customers[0].transform.position;
        money.transform.SetParent(transform);
        money.transform.DOLocalMove(GetMoneyTargetPos(), 1f);
        money.transform.DORotate(new Vector3(0, Random.Range(100, 360), 0), 1);
        money.transform.DOScale(money.transform.localScale.x + .2f, 1).SetLoops(-1, LoopType.Yoyo);
        moneyList.Add(money);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (isPlayer)
        {
            foreach (Money money in moneyList)
            {
                money.transform.DOMove(money.transform.position + GetRandomLootOffset(),
                    data.lootMoveUpDuration)
                    .OnComplete(() =>
                    {
                        money.SetGoToPlayer();
                    });

                money.transform.DORotate(new Vector3(Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)), data.lootRotDuration);

                money.transform.DOScale(.4f, data.lootScaleDuration);
            }
            moneyList.Clear();
        }
    }


    Vector3 GetMoneyTargetPos()
    {        
        return new Vector3(Random.Range(-dropMoneyOffset, dropMoneyOffset),
            transform.position.y,
            Random.Range(-dropMoneyOffset, dropMoneyOffset));
    }

    Vector3 GetRandomLootOffset()
    {
        return new Vector3
            (Random.Range(-data.lootXOffset, data.lootXOffset),
            Random.Range(data.lootMinYOffset, data.lootMaxYOffset),
            Random.Range(-data.lootZOffset, data.lootZOffset));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isPlayer = false;
    }

}
