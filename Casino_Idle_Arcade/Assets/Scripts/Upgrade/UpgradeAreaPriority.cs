using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAreaPriority : MonoBehaviour
{

    [SerializeField] GameObject[] upgradeControllers;
                                                    
    public void ActiveUpgradeController(int upgradeIndex)
    {
        upgradeControllers[upgradeIndex].SetActive(true);
    }

}
