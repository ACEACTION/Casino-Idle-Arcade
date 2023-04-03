using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMoneyTest : MonoBehaviour
{
    [SerializeField] StackMoney stackResource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Money money = StackMoneyPool.Instance.pool.Get();
            money.transform.position = transform.position;
            stackResource.AddToStack(money);
        }
    }


}
