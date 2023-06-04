using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUpgradePanel : MonoBehaviour
{
    public static MainUpgradePanel Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
