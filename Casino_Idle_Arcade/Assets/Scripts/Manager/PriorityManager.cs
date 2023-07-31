using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    [SerializeField] GameObject boughtElement;
    [SerializeField] bool followCam;
    public List<GameObject> priorityObjs;


    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;

        SetPrioritySlotState(true);
        
        PriorityController.Instance.AddPriority(priorityObjs, boughtElement);


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



