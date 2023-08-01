using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyArea_CasinoElement : MonoBehaviour
{
    [SerializeField] CasinoElement element;
    [SerializeField] BuyAreaController bA;
    bool notSaved;

    private void Start()
    {
        StartCoroutine(Delay());
    }


    // if element bought, we need to disappear buyarea (user bought and saved it)
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.1f);
        if (element.gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (bA.price <= 0 && !notSaved) 
        {
            
            notSaved = true;
            SaveLoad_CasinoElement.Instance.AddItemToElementsSaveDatas(element);
        }
    }

}
