using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ElementStack : MonoBehaviour
{
    // variables
    [SerializeField] int maxStackCount;
    public int stackCount;
    // references
    public CasinoGameStackData data;
    public List<CasinoResource> casinoResources = new List<CasinoResource>();
    public Transform firsStack;
    [SerializeField] TextMeshPro stackTxt;
    [SerializeField] Transform resourceIcon;
    [SerializeField] Transform ground;

    public virtual void Start()
    {
        data.iconDefaultScale = resourceIcon.localScale.x;
        data.stackDefaultScale = transform.localScale.x;
        ShowEmptyStack();
        SetStackTxt();
    }

    public bool CanAddStack() => stackCount < maxStackCount;
    public bool CanGetResource() => casinoResources.Count > 0;
    public void SetMaxStackCount(int levelIndex)
    {
        maxStackCount = data.maxStackCountLevel[levelIndex];
        SetStackTxt();
    }
    public int GetStackCount() => stackCount;
    public int GetMaxStackCount() => maxStackCount;
    public bool StackIsEmpty() => stackCount == 0;
    public void SetStackTxt() => stackTxt.text = string.Concat(stackCount.ToString(), "/", maxStackCount);

    public void ShowEmptyStack()
    {
        if (!CanGetResource())
        {
            //resourceIcon.DOScale(data.iconDefaultScale, .7f);
            ground.DOScale(data.stackDefaultScale + .2f, 1);
        }
    }

    public virtual void AddToGameStack(CasinoResource resource)
    {
        stackCount++;
        SetStackTxt();
        if (stackCount == 1)
        {
            ground.DOScale(data.stackDefaultScale, 1);
        }
    }

    public CasinoResource GetFromGameStack()
    {
        if (CanGetResource())
        {
            stackCount--;
            firsStack.position -= new Vector3(0, data.stackYOffset, 0);
            CasinoResource resource = casinoResources[casinoResources.Count - 1];
            casinoResources.RemoveAt(casinoResources.Count - 1);
            resource.transform.SetParent(null);
            SetStackTxt();            
            ShowEmptyStack();
            SetDeliverProcess();
            return resource;
        }
        else
        {
            return null;
        }
    }
  
    public virtual void SetDeliverProcess()
    {
        
    }

}
