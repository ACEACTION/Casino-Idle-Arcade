using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class CasinoGameStack : MonoBehaviour
{
    // variables
    [SerializeField] CasinoGameStackData data;
    [SerializeField] int maxStackCount;
    [SerializeField] int stackCount;
    [SerializeField] CasinoGame game;
    // references
    [SerializeField] List<CasinoResource> casinoResources = new List<CasinoResource>();
    [SerializeField] Transform firsStack;
    [SerializeField] TextMeshPro stackTxt;
    [SerializeField] Transform resourceIcon;
    [SerializeField] Transform ground;
    private void Start()
    {
        data.iconDefaultScale = resourceIcon.localScale.x;
        data.stackDefaultScale = transform.localScale.x;
        ShowEmptyStack();
        SetStackTxt();
    }

    public bool CanAddStack() => stackCount < maxStackCount;
    public bool CanGetResource() => casinoResources.Count > 0;

    public void AddToGameStack(CasinoResource resource)
    {
        stackCount++;
        resource.transform.SetParent(firsStack.transform.parent);
        resource.transform.DOLocalMove(firsStack.localPosition,
            data.addResourceToStackTime).OnComplete(() =>
            {
                resource.transform.DOShakeScale(0.0f, 0.0f).OnComplete(() => { resource.transform.DOScale(0.67f, 0.1f); });
                casinoResources.Add(resource);
            });

        firsStack.position += new Vector3(0, data.stackYOffset, 0);
        SetStackTxt();

        if (stackCount == 1)
        {
           // resourceIcon.DOScale(.01f, 1f);
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
            if (StackIsEmpty())
            {
                game.chipDeliverer.casinoGamesPoses.Add(game);
            }
            ShowEmptyStack();
            return resource;
        }
        else
        {
            return null;
        }
    }

    void ShowEmptyStack()
    {
        if (!CanGetResource())
        {
            //resourceIcon.DOScale(data.iconDefaultScale, .7f);
            ground.DOScale(data.stackDefaultScale + .2f, 1);
        }
    }

    public void SetMaxStackCount(int levelIndex)
    { 
        maxStackCount = data.maxStackCountLevel[levelIndex];
        SetStackTxt();
    }

    void SetStackTxt() => stackTxt.text = string.Concat(stackCount.ToString(), "/", maxStackCount);

    public int GetStackCount() => stackCount;



    public int GetMaxStackCount() => maxStackCount;


    public bool StackIsEmpty()
    {
        return stackCount == 0;
    }
}
