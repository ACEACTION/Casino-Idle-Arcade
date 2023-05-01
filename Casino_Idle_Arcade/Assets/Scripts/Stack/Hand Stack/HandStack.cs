using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandStack : MonoBehaviour
{       

    // variables
    public int maxStackCount;
    [SerializeField] int stackCount;
    [SerializeField] float stackYOffset;
    public float maxAddStackCd;
    [SerializeField] CapsuleCollider collider;
    float addStackCd;
    [SerializeField] float maxRemoveStackCd;
    float removeStackCd;
    public bool stackHasResource;

    // references
    [SerializeField] Animator anim;
    public Transform firstStack;
    public List<CasinoResource> stackList;
    public CasinoResourceDesk casinoResource;
    CasinoGameStack casinoGameStack;

    private void Start()
    {
        addStackCd = maxAddStackCd;
    }

    private void Update()
    {
        AddStackResourceWithCd();
        RemoveFromStackWithCd();
        SetStackAnimation();
    }

    public bool CanAddStack() => stackCount < maxStackCount;
    bool CanRemoveStack() => stackCount > 0;

    void AddStackResourceWithCd()
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
    }
    void AddStackResource()
    {
        if (casinoResource)
        {            
            casinoResource.AddResourceToStack(this, stackList);
            stackCount++;
            AudioSourceManager.Instance.PlayPoPSfx();
            firstStack.transform.localPosition += new Vector3(0, stackYOffset, 0);
            stackHasResource = true;
        }
    }

    void SetStackAnimation()
    {
        if (stackHasResource)
            anim.SetLayerWeight(1, 1);
        else
            anim.SetLayerWeight(1, 0.1f);

    }


    void RemoveFromStackWithCd()
    {
        if (casinoGameStack && CanRemoveStack() && collider.enabled == true)
        {
            removeStackCd -= Time.deltaTime;
            if (removeStackCd < 0)
            {
                RemoveFromStack();
                removeStackCd = maxRemoveStackCd;
            }
        }
    }
    void RemoveFromStack()
    {
        if (casinoGameStack && casinoGameStack.CanAddStack())
        {
            casinoGameStack.AddToGameStack(stackList[stackList.Count - 1]);
            stackList.RemoveAt(stackList.Count - 1);
            stackCount--;
            firstStack.transform.localPosition -= new Vector3(0, stackYOffset, 0);
            AudioSourceManager.Instance.PlayPoPSfx();
            stackHasResource = CanRemoveStack();
        }
    }


    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Casino Resource"))
        {            
            casinoResource = other.GetComponent<CasinoResourceDesk>();
        }

        if (other.gameObject.CompareTag("Casino Chip Game"))
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

        if (other.gameObject.CompareTag("Casino Chip Game"))
        {
            casinoGameStack = null;
        }

    }

    public int GetStackCount() => stackCount;
}
