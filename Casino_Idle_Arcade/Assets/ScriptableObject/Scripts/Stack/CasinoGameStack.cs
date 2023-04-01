using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CasinoGameStack : MonoBehaviour
{
    // variables
    [SerializeField] float stackYOffset;
    [SerializeField] int maxStackCount;
    [SerializeField] int stackCount;

    // references
    [SerializeField] List<CasinoResource> casinoResources = new List<CasinoResource>();
    [SerializeField] Transform firsStack;

    public bool CanAddStack() => stackCount < maxStackCount;
    public bool CanGetResource() => stackCount > 0;

    public void AddToGameStack(CasinoResource resource)
    {
        casinoResources.Add(resource);
        resource.transform.SetParent(firsStack.transform.parent);
        resource.transform.DOLocalMove(firsStack.localPosition, resource.data.removeStackSpeed);
        firsStack.position += new Vector3(0, stackYOffset, 0);
        stackCount++;
    }

    public CasinoResource GetFromGameStack()
    {
        if (CanGetResource())
        {
            stackCount--;
            firsStack.position -= new Vector3(0, stackYOffset, 0);
            CasinoResource resource = casinoResources[casinoResources.Count - 1];
            casinoResources.RemoveAt(casinoResources.Count - 1);
            resource.transform.SetParent(null);            
            return resource;
        }
        else return null;
    }

}
