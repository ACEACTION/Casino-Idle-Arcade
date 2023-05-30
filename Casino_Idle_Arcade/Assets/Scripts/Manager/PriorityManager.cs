using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    [SerializeField] bool followCam;
    public List<GameObject> priorityObjs;


    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;

        SetPrioritySlotState(true);

        if (followCam)
        {
            UpgradeCameraFollow.instance.destinations = priorityObjs;
            UpgradeCameraFollow.instance.DoCoroutine();
        }
    }

    void SetPrioritySlotState(bool state)
    {
        foreach (GameObject e in priorityObjs)
        {
            e?.SetActive(state);
        }
    }
}



