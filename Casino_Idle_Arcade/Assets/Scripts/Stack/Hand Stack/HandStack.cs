using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandStack : MonoBehaviour
{

    // variables    
    [SerializeField] int stackCount;    
    float addStackCd;
    float removeStackCd;
    [SerializeField] List<ResourceDeskType> resourcesToStackType;

    // for test
    int defaultMaxStackCount;


    // references
    public HandStackData data;
    [SerializeField] Animator anim;
    public Transform firstStack;    
    public List<CasinoResource> stackList;
    public List<CasinoResource> vMachineList;
    public List<CasinoResource> chipList;
    public CasinoResourceDesk resourceDesk;
    [HideInInspector] public ElementStack casinoGameStack;
    ElementStack vMachineStack;    


    private void Start()
    {
        addStackCd = data.maxAddStackCd;
        data.firsStackOrigin = firstStack.localPosition;

        // for test
        defaultMaxStackCount = data.maxStackCount;
    }

    private void Update()
    {
        AddStackResourceWithCd();
        RemoveFromStackWithCd();
    }

    public bool CanAddStack() => stackCount < data.maxStackCount;
    public bool CanRemoveStack() => stackCount > 0;

    void AddStackResourceWithCd()
    {
        if (resourceDesk && CanAddStack())
        {
            addStackCd -= Time.deltaTime;
            if (addStackCd < 0)
            {
                AddStackResource();
                addStackCd = data.maxAddStackCd;
            }
        }
    }
    
    public void AddStackResource()
    {
        if (resourceDesk && resourcesToStackType.Contains(resourceDesk.deskType))
        {
            AddStackResourceProcess();           
        }
    }


    public virtual void AddStackResourceProcess()
    {
        resourceDesk.AddResourceToStack(this);
        stackCount++;
        AudioSourceManager.Instance.PlayPoPSfx();
        firstStack.transform.localPosition += new Vector3(0, data.stackYOffset, 0);

        // set animation
        anim.SetLayerWeight(1, 1);
    }



    public virtual void RemoveFromStackWithCd()
    {
        if ((casinoGameStack || vMachineStack) && CanRemoveStack())
        {
            RemoveFromStackWithCdProcess();    
        }        
    }

    public void RemoveFromStackWithCdProcess()
    {
        removeStackCd -= Time.deltaTime;
        if (removeStackCd < 0)
        {
            RemoveFromStack();
            removeStackCd = data.maxRemoveStackCd;
        }
    }

    public virtual void RemoveFromStack()
    {
        if (casinoGameStack && ListHasResource(chipList) && casinoGameStack.CanAddStack())
        {
            CasinoResource resource = chipList[chipList.Count - 1];
            RemoveFromStackProcess(resource);
            casinoGameStack.AddToGameStack(resource);
            chipList.Remove(resource);
        }

        if (vMachineStack && ListHasResource(vMachineList) && vMachineStack.CanAddStack())
        {
            CasinoResource resource = vMachineList[vMachineList.Count - 1];
            RemoveFromStackProcess(resource);
            vMachineStack.AddToGameStack(resource);
            vMachineList.Remove(resource);
        }

        

    }

    public virtual void RemoveFromStackProcess(CasinoResource resource)
    {
        stackList.Remove(resource);
        stackCount--;
        firstStack.transform.localPosition -= new Vector3(0, data.stackYOffset, 0);
        AudioSourceManager.Instance.PlayPoPSfx();
        ResetStackListPos();    
        
        // set animation
        if (StackIsEmpty()) anim.SetLayerWeight(1, 0.01f);

    }

    void ResetStackListPos()
    {
        firstStack.localPosition = data.firsStackOrigin;
        foreach (CasinoResource r in stackList)
        {
            r.transform.localPosition = firstStack.localPosition;
            firstStack.transform.localPosition += new Vector3(0, data.stackYOffset, 0);
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Casino Resource"))
        {            
            resourceDesk = other.GetComponent<CasinoResourceDesk>();
        }

        if (other.gameObject.CompareTag("Casino Chip Game"))
        {
            casinoGameStack = other.GetComponent<CasinoGameStack>();
        }

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = other.GetComponent<VendingMachineStack>();
        }

        

    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Casino Resource"))
        {
            resourceDesk = null;
        }

        if (other.gameObject.CompareTag("Casino Chip Game"))
        {
            casinoGameStack = null;
        }

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = null;
        }        
    }

    public int GetStackCount() => stackCount;
    public void AddMaxStackCounter(int amount)
    {
        data.maxStackCount += amount;
        MaxStackText.Instance.SetTextState(false);
    }
    public bool StackIsMax() => stackCount == data.maxStackCount;
    public bool StackIsEmpty() => stackCount == 0;
    public bool ListHasResource<T>(List<T> list) => list.Count > 0;

    private void OnDisable()
    {
        data.maxStackCount = defaultMaxStackCount;
    }

}
