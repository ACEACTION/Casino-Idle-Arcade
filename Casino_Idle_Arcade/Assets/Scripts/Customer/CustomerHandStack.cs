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

    public void AddResourceToStack(CasinoResource resource)
    {
        resources.Add(resource);
        resource.transform.SetParent(transform);
        resource.transform.DOLocalRotate(Vector3.zero, 0);
        resource.transform.DOLocalMove(firstStack.localPosition, .4f).OnComplete(() =>
        {
        });
        firstStack.position += new Vector3(0, data.stackYOffset, 0);        
        stackCounter++;
    }


    public void RemoveChipsFromStack(ChipDesk chipDesk)
    {
        StartCoroutine(RemoveChipsWithDelay(chipDesk));
    }

    IEnumerator RemoveChipsWithDelay(ChipDesk chipDesk)
    {
        for (int i = resources.Count - 1; i >= 0; i--)
        {
            stackCounter--;
            firstStack.position -= new Vector3(0, data.stackYOffset, 0);
            yield return new WaitForSeconds(data.removeChipDelay);
            CasinoResource resource = resources[i];
            resource.transform.SetParent(null);
            resource.transform.DOMove(chipDesk.chipSpawnPoint.position, data.removeChipToDeskTime)
                .OnComplete(() =>
                {
                    resource.ReleaseResource();
                });
            resource.releaseResource = true;
            chipDesk.AddReleaseChipList(resources[i]);
        }

        resources.Clear();
        //chipDesk.ReleaseChips();

        // stack money in customer hand
        GetMoneyFromChipDesk(chipDesk);
        cmovement.ExitCasino();
    }

    public void ReleaseResources()
    {
        foreach (CasinoResource r in resources)
        {     
            r.ReleaseResource();
        }

        stackCounter = 0;
        firstStack.localPosition = data.firstStackPos;
        resources.Clear();
    }

    void GetMoneyFromChipDesk(ChipDesk chipDesk)
    {
        Money money = StackMoneyPool.Instance.pool.Get();
        money.transform.position = transform.position;
        AddResourceToStack(money);
    }



    void GiveMoneyToChipDesk(ChipDesk chipDesk)
    {
        //cmovement.PayMoney(chipDesk.stackMoney, 4, MoneyType.chipDeskMoney);
    }

}

