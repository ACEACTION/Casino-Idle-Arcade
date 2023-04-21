using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoResource : MonoBehaviour
{       
    //public CasinoResourceData data;
    public virtual void ReleasResource()
    {
        transform.SetParent(null);

    }
}
