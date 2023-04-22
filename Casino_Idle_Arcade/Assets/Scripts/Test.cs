using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public CasinoResource casinoResource;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            casinoResource.ReleaseResource();
        }
    }
}
