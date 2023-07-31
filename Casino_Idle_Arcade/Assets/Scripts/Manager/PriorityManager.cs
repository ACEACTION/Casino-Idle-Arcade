using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    [SerializeField] GameObject openedPriority;
    [SerializeField] bool followCam;
    public List<GameObject> priorityObjs;
    bool setPriority;

    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;


        if (!setPriority)
        {
            setPriority = true;
            SetPrioritySlotState(true);
            
            PriorityController.Instance.AddPriority(priorityObjs, openedPriority);
        }

        if (followCam)
        {
            CameraFollow.instance.destinations = priorityObjs;
           // CameraFollow.instance.DoCoroutine();
        }
    }

    void SetPrioritySlotState(bool state)
    {
        foreach (GameObject e in priorityObjs)
        {
            if (e != null)
                e?.SetActive(state);
        }
    }
}



