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
    public ElementStackData data;
    public List<CasinoResource> casinoResources = new List<CasinoResource>();
    public Transform firsStack;
    [SerializeField] TextMeshProUGUI stackTxt;
    [SerializeField] Transform resourceIcon;
    [SerializeField] Transform ground;
    Vector3 firstStackOriginPos;

    public virtual void Start()
    {
        data.iconDefaultScale = resourceIcon.localScale.x;
        data.stackDefaultScale = transform.localScale.x;
        SetStackTxt();
        //resourceIconScale = resourceIcon.transform.localScale;
        ShowEmptyStack();
        firstStackOriginPos = firsStack.localPosition;
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
            //resourceIcon.gameObject.SetActive(true);
            //resourceIcon.DOScale(resourceIconScale, data.scaleDuration);
            ground.DOScale(data.stackDefaultScale + .2f, 1);

        }
    }

    public void SetResourceParent(Transform resource, Transform parent) => resource.parent = parent; 
    
    public void RotateResource(Transform resource)
        => resource.DORotate(new Vector3(0, Random.Range(0, 1200), 0), data.jumpDuration, RotateMode.FastBeyond360);

    public void JumpMoveResource(CasinoResource resource) 
        => resource.transform.DOJump(firsStack.position, data.jumpPower, 1,
            data.jumpDuration).OnComplete(() =>
            {
                CompleteJumpMove(resource);
            });
    public virtual void CompleteJumpMove(CasinoResource resource) 
    {
        casinoResources.Add(resource);
        //resourceIcon.DOScale(new Vector3(.01f, .01f, .01f), data.scaleDuration).OnComplete(() =>
        //{
        //    resourceIcon.gameObject.SetActive(false);
        //});
    }

    public virtual void AddToGameStack(CasinoResource resource)
    {
        stackCount++;
        SetStackTxt();
        resource.transform.eulerAngles = Vector3.zero;

        if (stackCount == 1)
        {
            ground.DOScale(data.stackDefaultScale, 1);
        }

        ResetStackListPos();
    }

    public CasinoResource GetFromGameStack()
    {

        if (CanGetResource())
        {
            stackCount--;
            GetResource();
            CasinoResource resource = casinoResources[casinoResources.Count - 1];
            casinoResources.RemoveAt(casinoResources.Count - 1);
            resource.transform.SetParent(null);
            SetStackTxt();            
            ShowEmptyStack();
            
            ResetStackListPos();

            return resource;
        }
        else
        {
            return null;
        }

    }

    public virtual void GetResource()
    {
    }

    public virtual void SetDeliverProcess()
    {
        
    }

    public void ResetStackListPos()
    {
        firsStack.localPosition = firstStackOriginPos;
        foreach (CasinoResource r in casinoResources)
        {
            r.transform.localPosition = firsStack.localPosition;
            firsStack.transform.localPosition += new Vector3(0, data.stackYOffset, 0);
        }
    }

}
