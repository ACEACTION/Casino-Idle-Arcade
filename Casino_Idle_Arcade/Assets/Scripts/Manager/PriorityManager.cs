using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    public List<PrioritySlot> prioritySlots;
    public List<GameObject> priorityObjs;
    public int priorityCount;
    public bool completePriority;

    public static PriorityManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }    


    public void OpenNextPriority()
    {
        if (!GameManager.isCompleteTutorial) return;
        SetPrioritySlotState(true);
    }

    void SetPrioritySlotState(bool state)
    {
        foreach (GameObject e in priorityObjs)
        {
            e.SetActive(state);
        }
    }
}


[System.Serializable]
public class PrioritySlot
{
    public List<GameObject> elements = new List<GameObject>();
}
