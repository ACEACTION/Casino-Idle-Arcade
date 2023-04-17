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
        if (completePriority || !GameManager.isCompleteTutorial) return;
        
        priorityCount++;
        SetPrioritySlotState(true);

        if (priorityCount == prioritySlots.Count - 1) completePriority = true;
    }


    public void OpenNext()
    {
        if (!GameManager.isCompleteTutorial) return;
        SetPrioritySlotState(true);
    }

    void SetPrioritySlotState(bool state)
    {
        //for (int i = 0; i < prioritySlots[0].elements.Count; i++)
        //{
                
        //}
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
