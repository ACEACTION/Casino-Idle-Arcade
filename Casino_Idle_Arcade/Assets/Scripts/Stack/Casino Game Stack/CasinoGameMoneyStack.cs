using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CasinoGameMoneyStack : MonoBehaviour
{
    [SerializeField] float dropMoneyOffset;
    public bool isPlayer;
    public int totalMoney;

    public LootMoneyData data;
    public CasinoElement element;
    public List<Money> moneyList = new List<Money>();
    [SerializeField] StackMoneyCanvas stackMoneyCanvas;

    public virtual void MakeMoney() {}
    
    

    public Vector3 GetMoneyTargetPos()
    {        
        return new Vector3(Random.Range(-dropMoneyOffset, dropMoneyOffset),
            transform.position.y,
            Random.Range(-dropMoneyOffset, dropMoneyOffset));
    }

    public Vector3 GetRandomLootOffset()
    {
        return new Vector3
            (Random.Range(-data.lootXOffset, data.lootXOffset),
            Random.Range(data.lootMinYOffset, data.lootMaxYOffset),
            Random.Range(-data.lootZOffset, data.lootZOffset));
    }


    void Update()
    {
        if (isPlayer)
        {
            LootMoney();
        }
    }

    void LootMoney()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            
            if (totalMoney > 0)
            {
                stackMoneyCanvas.AddMoneyText(totalMoney);
                totalMoney = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isPlayer = false;
    }

}
