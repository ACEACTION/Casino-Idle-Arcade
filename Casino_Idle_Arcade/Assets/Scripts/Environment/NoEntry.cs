using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntry : MonoBehaviour
{
    public bool isBought = true;

    public GameObject[] transparentPlanes;

    [SerializeField] BuyAreaController area;




    private void Update()
    {
        if (isBought && area.price <= 0)
        {
            isBought = false;

            transparentPlanes[0].transform.DOMoveX(transform.position.x + 30f, 3f).OnComplete(()=> { this.gameObject.SetActive(false); });
            transparentPlanes[1].transform.DOMoveX(transform.position.x - 30f, 3f);

        }
    }
}
