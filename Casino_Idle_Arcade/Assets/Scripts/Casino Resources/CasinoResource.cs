using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CasinoResource : MonoBehaviour
{
    //public CasinoResourceData data;
    public bool releaseResource;

    public virtual void ReleaseResource()
    {
        transform.SetParent(null);
        releaseResource = false;
    }
}
