using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityController : MonoBehaviour
{

    const string openedPrioritiesDataPath = "openedPrioritiesData";

    public List<PriorityManagerSlot> allPriorityManager;
    public List<GameObject> openedPriorities;
    public List<int> prioritiesId = new List<int>();
    public static PriorityController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //for (int i = 0; i < allPriorityManager.Count; i++)
        //{
        //    allPriorityManager[i].id = i;
        //}
    }


    public void AddPriority(List<GameObject> priorities, GameObject parent)
    {

        //foreach (GameObject obj in priorities)
        //{
        //    if (!openedPriorities.Contains(obj))
        //        openedPriorities.Add(obj);
        //}

        //for (int i = 0; i < priorities.Count; i++)
        //{
        //    for (int j = 0; j < allPriorityManager.Count; j++)
        //    {
        //        if (priorities[i].i)
        //    }
        //}


        if (openedPriorities.Contains(parent))
            openedPriorities.Remove(parent);

    }


    void SaveElementData()
    {
        //SaveLoadSystem.SaveAes(, openedPrioritiesDataPath,
        //    (error) => {
        //        Debug.Log(error);
        //    },
        //    (success) => {
        //        Debug.Log(success);
        //    });
    }


}

[System.Serializable]
public class PriorityManagerSlot
{
    public int id;
    public PriorityManager priorityManager;
}
