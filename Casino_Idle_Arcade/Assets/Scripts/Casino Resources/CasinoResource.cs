using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CasinoResource : MonoBehaviour
{
    //public CasinoResourceData data;
    public bool releaseResource;
    public Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
    }


    public virtual void ReleaseResource()
    {
        transform.SetParent(null);
        releaseResource = false;
    }

    public void ResetScale()
    {
        transform.localScale = defaultScale;
    }

}
