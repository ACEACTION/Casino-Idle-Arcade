using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : CasinoGame
{
    [SerializeField] VendingMachineStack stack;
    [SerializeField] GameObject snackDeskResource;
    bool payedMoney = true;

    public override void Start()
    {
        base.Start();
        vendingMachineManager.vendingMachines.Add(this);
        if (!CasinoManager.instance.availableElements.Contains(ElementsType.VendingMachine))
        {
            CasinoManager.instance.availableElements.Add(ElementsType.VendingMachine);
        }

        ShakeModel();

        snackDeskResource.SetActive(true);
    }

    private void Update()
    {
        if (readyToUse && stack.CanGetResource())
        {
            castTime -= Time.deltaTime;
            if(castTime <= 0)
            {
                PlayGame();
                EndGame();

                StartCoroutine(ResetGame());
            }
        }
    }

    public override IEnumerator ResetGame()
    {
        payedMoney = true;
        return base.ResetGame();
    }
    public override void PlayGame()
    {
        base.PlayGame();
        GiveSnackToCustomer();
     //   customers[0].SetPlayingVendingMachineAnimation(true);
    }
    public void EndGame()
    {
        customers[0].SetMove(ExitPosition.instance.customerSpot);
    }

    public void GiveSnackToCustomer()
    {
        CasinoFood food = (CasinoFood)stack.GetFromGameStack();
        food.transform.position = transform.position;
        food.MoveSnackToCustomer(customers[0].snackTransform);
        customers[0].stack.AddResourceToStack(food);
        PayMoney();

    }

    //void ShakeElement() => transform.DOShakeScale(1f, 0.5f);
    public void PayMoney()
    {
        if (payedMoney)
        {
            payedMoney = false;
            int moneyAmount = Random.Range(3, 5);
            customers[0].PayMoney(stackMoney, moneyAmount, MoneyType.vendingMoney);
        }
    }
    public override void CustomerHasArrived()
    {
        base.CustomerHasArrived();

        customerCounter++;
    }

    public override void CustomerLeft()
    {
        base.CustomerLeft();
        customerCounter--;
    }
}
