using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JackpotMoneyStack : CasinoGameMoneyStack
{
    public override void MakeMoney()
    {
        base.MakeMoney();    
        Money money = StackMoneyPool.Instance.pool.Get();
        moneyList.Add(money);
        money.transform.position = element.customers[0].transform.position;
        money.transform.SetParent(transform);
        money.transform.DOLocalMove(GetMoneyTargetPos(), 1f);
        money.transform.DORotate(new Vector3(0, Random.Range(100, 360), 0), 1);
        money.SetMoneyAmount(MoneyType.jackpotMoney);
        totalMoney += money.moneyAmount;
    }


}

