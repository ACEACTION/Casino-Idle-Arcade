using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandStack : MonoBehaviour
{

    // variables
    public int maxStackCount;
    [SerializeField] int stackCount;
    [SerializeField] float stackYOffset;
    public float maxAddStackCd;
    float addStackCd;
    [SerializeField] float maxRemoveStackCd;
    float removeStackCd;
    public bool stackHasResource;
    [SerializeField] bool activeMaxTxt;
    bool isTrash;

    // references
    [SerializeField] Animator anim;
    Transform trash;
    public Transform firstStack;
    Vector3 firsStackOrigin;
    public List<CasinoResource> stackList;
    public List<CasinoResource> vMachineList;
    public List<CasinoResource> chipList;
    CasinoResourceDesk casinoResource;
    [HideInInspector] public ElementStack casinoGameStack;
    ElementStack vMachineStack;
    [SerializeField] GameObject maxTxt;


    private void Start()
    {
        addStackCd = maxAddStackCd;
        firsStackOrigin = firstStack.localPosition;
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
            casinoResource.AddResourceToStack(this);
            stackCount++;
            AudioSourceManager.Instance.PlayPoPSfx();
            firstStack.transform.localPosition += new Vector3(0, stackYOffset, 0);
            stackHasResource = true;

            if (StackIsMax() && activeMaxTxt)
            {
                MaxStackText.Instance.SetTextState(true);
            }
        }
    }

    void SetStackAnimation()
    {
        if (stackHasResource)
            anim.SetLayerWeight(1, 1);
        else
            anim.SetLayerWeight(1, 0.01f);

    }


    void RemoveFromStackWithCd()
    {
        if ((casinoGameStack || vMachineStack || isTrash) && CanRemoveStack())
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

        if (isTrash)
        {            
            CasinoResource resource = stackList[stackList.Count - 1];
            resource.transform.DOJump(trash.position, 3, 1, .6f)
                .OnComplete(() =>
                {
                    resource.ReleaseResource();
                });

            chipList.Remove(resource);
            vMachineList.Remove(resource);
            RemoveFromStackProcess(resource);
        }

    }

    void RemoveFromStackProcess(CasinoResource resource)
    {
        stackList.Remove(resource);
        stackCount--;
        firstStack.transform.localPosition -= new Vector3(0, stackYOffset, 0);
        AudioSourceManager.Instance.PlayPoPSfx();
        stackHasResource = CanRemoveStack();
        ResetStackListPos();

        if (activeMaxTxt)
            MaxStackText.Instance.SetTextState(false);
    }

    void ResetStackListPos()
    {
        firstStack.localPosition = firsStackOrigin;
        foreach (CasinoResource r in stackList)
        {
            r.transform.localPosition = firstStack.localPosition;
            firstStack.transform.localPosition += new Vector3(0, stackYOffset, 0);
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

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = other.GetComponent<VendingMachineStack>();
        }

        if (other.gameObject.CompareTag("Trash"))
        {
            isTrash = true;
            trash = other.transform;
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

        if (other.gameObject.CompareTag("VendingMachine"))
        {
            vMachineStack = null;
        }

        if (other.gameObject.CompareTag("Trash"))
        {
            isTrash = false;            
        }
    }

    public int GetStackCount() => stackCount;
    public void AddMaxStackCounter(int amount)
    {
        maxStackCount += amount;
        MaxStackText.Instance.SetTextState(false);
    }
    public bool StackIsMax() => stackCount == maxStackCount;
    public bool ListHasResource<T>(List<T> list) => list.Count > 0;
}
