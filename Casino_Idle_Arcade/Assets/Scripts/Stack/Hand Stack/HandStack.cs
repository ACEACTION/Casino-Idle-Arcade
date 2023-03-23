using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandStack : MonoBehaviour
{       

    // variables
    [SerializeField] int maxStackCount;
    [SerializeField] int stackCount;
    [SerializeField] float stackYOffset;
    [SerializeField] float maxAddStackCd;
    float addStackCd;
    [SerializeField] float maxRemoveStackCd;
    float removeStackCd;
    public bool stackHasResource;

    // references
    public Transform firstStack;
    public List<CasinoResource> stackList;
    CasinoResourceDesk casinoResource;
    CasinoGameStack casinoGameStack;

    private void Start()
    {
        
        addStackCd = maxAddStackCd;
    }

    private void Update()
    {
        if (casinoResource && CanAddStack())
        {            
            addStackCd -= Time.deltaTime;
            if (addStackCd < 0)
            {
                AddStackResource();
                addStackCd = maxAddStackCd;
            }
        }

        if (casinoGameStack && CanRemoveStack())
        {
            removeStackCd -= Time.deltaTime;
            if (removeStackCd < 0)
            {
                RemoveFromStack();
                removeStackCd = maxRemoveStackCd;
            }
        }

    }

    bool CanAddStack() => stackCount < maxStackCount;
    bool CanRemoveStack() => stackCount > 0;

    void AddStackResource()
    {
        if (casinoResource)
        {
            casinoResource.AddResourceToStack(this, stackList);
            stackCount++;
            firstStack.transform.position += new Vector3(0, stackYOffset, 0);
            stackHasResource = true;
        }
    }

    void RemoveFromStack()
    {
        if (casinoGameStack && casinoGameStack.CanAddStack())
        {
            casinoGameStack.AddToGameStack(stackList[stackList.Count - 1]);
            stackList.RemoveAt(stackList.Count - 1);
            stackCount--;
            firstStack.transform.position -= new Vector3(0, stackYOffset, 0);

            stackHasResource = CanRemoveStack();
        }
    }


    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Casino Resource"))
        {            
            casinoResource = other.GetComponent<CasinoResourceDesk>();
        }

        if (other.gameObject.CompareTag("Roulette"))
        {
            casinoGameStack = other.GetComponent<CasinoGameStack>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Casino Resource"))
        {
            casinoResource = null;
        }

        if (other.gameObject.CompareTag("Roulette"))
        {
            casinoGameStack = null;
        }

    }

}
