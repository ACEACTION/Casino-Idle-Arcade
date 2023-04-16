using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    public List<PrioritySlot> prioritySlots;
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

    void SetPrioritySlotState(bool state)
    {
        foreach (GameObject e in prioritySlots[priorityCount].elements)
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
