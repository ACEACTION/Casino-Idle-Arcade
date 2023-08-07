using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoaderCanvas : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
