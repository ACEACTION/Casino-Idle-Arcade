using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RouletteMoneyStack : CasinoGameMoneyStack
{

    public override void MakeMoney()
    {
        base.MakeMoney();                
        
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = element.customers
            [Random.Range(0, element.customers.Count)].transform.position;
        money.transform.SetParent(transform);       
        money.SetMoneyAmount(MoneyType.rouletteMoney);
        totalMoney += money.moneyAmount;

        if (stackCounter <= stackData.maxStackCounter)
        {

            money.transform.DORotate(new Vector3(0, Random.Range(100, 360), 0), 1);
            money.transform.localScale -= new Vector3(.2f, .2f, .2f);
            money.transform.DOScale(money.transform.localScale
                + new Vector3(.1f, .1f, .1f), .7f)
                .SetLoops(-1, LoopType.Yoyo);
            money.transform.DOLocalMove(GetMoneyTargetPos(), 1f);

            moneyList.Add(money);
            money.ActiveEffect();
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
