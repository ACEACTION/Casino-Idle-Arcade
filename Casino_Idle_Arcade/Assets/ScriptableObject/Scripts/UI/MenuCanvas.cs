using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public static MenuCanvas instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
