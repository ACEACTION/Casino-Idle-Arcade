using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class PriorityController : MonoBehaviour
{

    const string openedPrioritiesDataPath = "openedPrioritiesData";

    public List<PriorityObjectSlot> allPriorityObjects;

    public List<PriorityObjectSlot> openedPriorities;
    public List<int> prioritiesId = new List<int>();
    public static PriorityController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadData();
    }

    private void Start()
    {
        //for (int i = 0; i < allPriorityObjects.Count; i++)
        //{
        //    allPriorityObjects[i].id = i;
        //}
        LoadPrioritiesId();
    }


    public void AddPriority(List<GameObject> priorities, GameObject boughtElement)
    {
        //for (int i = 0; i < priorities.Count; i++)
        //{
        //    for (int j = 0; j < allPriorityObjects.Count ; j++)
        //    {
        //        if (priorities[i] == allPriorityObjects[j].obj)
        //        {

        //        }
        //    }
        //}

        foreach (GameObject obj in priorities)
        {
            //openedPriorities.Add(new PriorityObjectSlot(GetId(obj), obj));
            prioritiesId.Add(GetId(obj));
        }

        int boughtElementId = GetId(boughtElement);

        if (boughtElementId == -1) return;

        foreach (int id in prioritiesId)
        {
            if (id == boughtElementId)
            {
                prioritiesId.Remove(id);
                break;
            }
        }

        SaveData();
    }


    int GetId(GameObject obj)
    {
        foreach (PriorityObjectSlot slot in allPriorityObjects)
        {
            if (slot.obj == obj)
                return slot.id;
        }
        return -1;      
    }

    void SaveData()
    {
        SaveLoadSystem.SaveAes(prioritiesId, openedPrioritiesDataPath,
            (error) =>
            {
                Debug.Log(error);
            },
            (success) =>
            {
                Debug.Log(success);
            });
    }

    void LoadPrioritiesId()
    {
        //LoadData();

        for (int i = 0; i < prioritiesId.Count; i++)
        {
            for (int j = 0; j < allPriorityObjects.Count; j++)
            {
                if (prioritiesId[i] == allPriorityObjects[j].id)
                {
                    allPriorityObjects[j].obj.SetActive(true);
                }
            }
        }

    }

    void LoadData()
    {
        SaveLoadSystem.LoadAes<List<int>>((data) =>
        {
            prioritiesId = data;
        }, openedPrioritiesDataPath
        , (error) => { Debug.Log(error); }
        , (success) => { Debug.Log(success); });

    }
}

[System.Serializable]
public class PriorityObjectSlot
{
    public int id;
    public GameObject obj;
    public PriorityObjectSlot(int id, GameObject obj)
    {
        this.id = id;
        this.obj = obj;
    }
}