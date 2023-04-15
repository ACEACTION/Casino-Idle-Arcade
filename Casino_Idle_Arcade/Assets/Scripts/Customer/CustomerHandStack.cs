using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerHandStack : MonoBehaviour
{
    // variables
    
    public int stackCounter;
    // references
    [SerializeField] CustomerHandStackData data;    
    [SerializeField] Transform firstStack;
    [SerializeField] List<CasinoResource> resources = new List<CasinoResource>();
    [SerializeField] CustomerMovement cmovement;

    private void Start()
    {
        data.firstStackPos = firstStack.localPosition;
    }

    public bool HasStack() => stackCounter > 0;

    public void AddChipToStack(CasinoResource chip)
    {
        resources.Add(chip);
        chip.transform.SetParent(transform);
        chip.transform.DOLocalMove(firstStack.localPosition, .7f);
        firstStack.position += new Vector3(0, data.stackYOffset, 0);
        stackCounter++;
    }


    public void RemoveFromStack(ChipDesk chipDesk)
    {
        StartCoroutine(RemoveWithDelay(chipDesk));
    }

    IEnumerator RemoveWithDelay(ChipDesk chipDesk)
    {
        for (int i = resources.Count - 1; i >= 0; i--)
        {
            stackCounter--;
            firstStack.position -= new Vector3(0, data.stackYOffset, 0);
            yield return new WaitForSeconds(data.removeChipDelay);
            resources[i].transform.SetParent(chipDesk.chipSpawnPoint);
            resources[i].transform.DOLocalMove(Vector3.zero, data.removeChipToDeskTime).OnComplete(() =>
            {
                resources[i].ReleasResource();
            });

        }

        resources.Clear();

        // stack money in customer hand
        GetMoneyFromChipDesk(chipDesk);
    }    

    public void ReleaseResources()
    {
        foreach (CasinoResource r in resources)
        {     
            r.ReleasResource();
        }

        stackCounter = 0;
        firstStack.localPosition = data.firstStackPos;
        resources.Clear();

    }

    void GetMoneyFromChipDesk(ChipDesk chipDesk)
    {
        Money money = chipDesk.GiveMoney();
        money.transform.SetParent(firstStack);
        money.transform.localScale += new Vector3(.1f, .1f, .1f);
        money.transform.
            DOLocalMove(Vector3.zero, 
            money.moneyData.moneyGoToCustomerFromDeskTime)
            .OnComplete(() =>
            {
                GiveMoneyToChipDesk(chipDesk);
            });
        resources.Add(money);
        stackCounter++;
        cmovement.ExitCasino();
    }


    void GiveMoneyToChipDesk(ChipDesk chipDesk)
    {
        cmovement.PayMoney(chipDesk.stackMoney, 1, MoneyType.chipDeskMoney);
    }

}

