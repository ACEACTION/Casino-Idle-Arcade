using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CasinoGameStack : MonoBehaviour
{
    // variables
    [SerializeField] CasinoGameStackData data;
    [SerializeField] int maxStackCount;
    [SerializeField] int stackCount;

    // references
    [SerializeField] List<CasinoResource> casinoResources = new List<CasinoResource>();
    [SerializeField] Transform firsStack;
    [SerializeField] TextMeshPro stackTxt;

    private void Start()
    {
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
                resource.transform.DOShakeScale(0.1f, 0.3f);
                casinoResources.Add(resource);
            });

        firsStack.position += new Vector3(0, data.stackYOffset, 0);
        SetStackTxt();
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
            return resource;
        }
        else return null;
    }

    public void SetMaxStackCount(int levelIndex)
    { 
        maxStackCount = data.maxStackCountLevel[levelIndex];
        SetStackTxt();
    }

    void SetStackTxt() => stackTxt.text = string.Concat(stackCount.ToString(), "/", maxStackCount);
}
