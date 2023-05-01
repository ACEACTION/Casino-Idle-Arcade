using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    public List<GameObject> priorityObjs;


    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;
        SetPrioritySlotState(true);
    }

    void SetPrioritySlotState(bool state)
    {
        foreach (GameObject e in priorityObjs)
        {
            e?.SetActive(state);
        }
    }
}



