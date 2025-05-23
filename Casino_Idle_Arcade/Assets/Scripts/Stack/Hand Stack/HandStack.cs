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
    public Animator anim;
    public Transform firstStack;    
    public List<CasinoResource> stackList;
    public List<CasinoResource> vMachineList;
    public List<CasinoResource> chipList;
    public CasinoResourceDesk resourceDesk;
    [HideInInspector] public ElementStack elementStack;
    ElementStack vMachineStack;    

    public virtual void Awake()
    {

    }

    void Start()
    {
        addStackCd = data.maxAddStackCd;
        data.firstStackOrigin = firstStack.localPosition;

        // for test
        //defaultMaxStackCount = data.maxStackCount;
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
        firstStack.transform.localPosition += new Vector3(0, data.stackYOffset, 0);

        // set animation
        //anim.SetLayerWeight(1, 1);
    }



    public virtual void RemoveFromStackWithCd()
    {
        if ((elementStack || vMachineStack) && CanRemoveStack())
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
        if (elementStack && ListHasResource(chipList) && elementStack.CanAddStack())
        {
            CasinoResource resource = chipList[chipList.Count - 1];
            RemoveFromStackProcess(resource);
            elementStack.AddToGameStack(resource);
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
        ResetStackListPos();    
        
        // set animation
        //if (StackIsEmpty()) anim.SetLayerWeight(1, 0.01f);

    }

    void ResetStackListPos()
    {
        firstStack.localPosition = data.firstStackOrigin;
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
            EnterToResourceDesk(other);
        }

        if (other.gameObject.CompareTag("Casino Chip Game"))
        {
            elementStack = other.GetComponent<CasinoGameStack>();
        }

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = other.GetComponent<VendingMachineStack>();
        }

    }

    public virtual void EnterToResourceDesk(Collider other)
    {
        resourceDesk = other.GetComponent<CasinoResourceDesk>();
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Casino Resource"))
        {
            ExitResourceDesk();
        }

        if (other.gameObject.CompareTag("Casino Chip Game"))
        {
            elementStack = null;
        }

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = null;
        }        
    }

    public virtual void ExitResourceDesk()
    {
        resourceDesk = null;
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

    public void SetFirstStack(Transform t) => firstStack = t;

    private void OnDisable()
    {
        //data.maxStackCount = defaultMaxStackCount;
    }

}
