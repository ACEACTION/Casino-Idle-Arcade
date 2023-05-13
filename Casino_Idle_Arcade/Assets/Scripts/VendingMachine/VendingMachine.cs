using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : CasinoGame
{

    private void Start()
    {
        vendingMachineManager.vendingMachines.Add(this);
        if (!CasinoManager.instance.availableElements.Contains(ElementsType.VendingMachine))
        {
            CasinoManager.instance.availableElements.Add(ElementsType.VendingMachine);
        }

        ShakeElement();


    }

    private void Update()
    {
        if (readyToPlay)
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
        Snack snack = VendingMachinePool.Instance.snackPool.Get();
        snack.transform.position = transform.position;
        snack.MoveSnackToCustomer(customers[0].snackTransform);
        customers[0].stack.AddResourceToStack(snack);



    }

    void ShakeElement() => transform.DOShakeScale(1f, 0.5f);

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
