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
        money.transform.position = element.customers[0].transform.position;
        money.transform.SetParent(transform);
        //MoveMoneyToStack(money);
        money.SetMoneyAmount(MoneyType.jackpotMoney);
        totalMoney += money.moneyAmount;

        if (stackCounter <= stackData.maxStackCounter)
        {
            money.transform.DOLocalMove(GetMoneyTargetPos(), 1f);
            moneyList.Add(money);
            money.transform.DORotate(new Vector3(0, Random.Range(100, 360), 0), 1);
        }
        else
        {
            money.transform.DOLocalMove(GetMoneyTargetPos(), 1f)
                .OnComplete(() =>
                {
                    money.ReleaseResource();
                });
        }

        stackCounter++;
    }


}

