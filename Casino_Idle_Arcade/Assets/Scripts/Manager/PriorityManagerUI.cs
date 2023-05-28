using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManagerUI : MonoBehaviour
{
    [SerializeField] BuyAreaController bac;
    [SerializeField] List<GameObject> priorityObjs;


    private void Update()
    {
        if (bac.price <= 0)
        {
            for (int i = 0; i < priorityObjs.Count; i++)
            {
                priorityObjs[i].SetActive(true);
            }
        }
    }

}
