using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class PriorityController : MonoBehaviour
{

    const string openedPrioritiesDataPath = "openedPrioritiesData";

    public List<PriorityObjectSlot> allPriorityObjects;
    public List<BuyAreaController> allBuyAreas;
    public List<PrioritySaveDataSlot> prioritiesSaveDataSlots = new List<PrioritySaveDataSlot>();
    public static PriorityController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadData();
    }

    private void Start()
    {
        for (int i = 0; i < allBuyAreas.Count; i++)
        {
            allPriorityObjects.Add(new PriorityObjectSlot(i, allBuyAreas[i]));
        }


        LoadPrioritiesId();
    }


    public void AddPriority(List<GameObject> priorities, GameObject boughtElement)
    {
        
        foreach (GameObject obj in priorities)
        {
            PriorityObjectSlot slot = GetPriorityObj(obj);
            if (slot != null)
                prioritiesSaveDataSlots.Add(new PrioritySaveDataSlot(slot.id, slot.bAC.price));
            else
                prioritiesSaveDataSlots.Add(new PrioritySaveDataSlot(-1, -1));
        }

        PriorityObjectSlot priorityObject = GetPriorityObj(boughtElement);

        if (priorityObject == null) return;

        foreach (PrioritySaveDataSlot slot in prioritiesSaveDataSlots)
        {
            if (slot.id == priorityObject.id)
            {
                prioritiesSaveDataSlots.Remove(slot);
                break;
            }
        }

        SaveData();
    }


    PriorityObjectSlot GetPriorityObj(GameObject obj)
    {
        foreach (PriorityObjectSlot slot in allPriorityObjects)
        {
            if (slot.bAC.priorityManager.openedPriority == obj)
                return slot;
        }
        return null;
    }

    public void SaveData()
    {
        for (int i = 0; i < prioritiesSaveDataSlots.Count; i++)
        {
            for (int j = 0; j < allPriorityObjects.Count; j++)
            {
                if (prioritiesSaveDataSlots[i].id == allPriorityObjects[j].id)
                {
                    prioritiesSaveDataSlots[i].remainPrice = allPriorityObjects[j].bAC.price;
                    break;
                }
            }
        }

        SaveLoadSystem.SaveAes(prioritiesSaveDataSlots, openedPrioritiesDataPath,
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
        for (int i = 0; i < prioritiesSaveDataSlots.Count; i++)
        {
            for (int j = 0; j < allPriorityObjects.Count; j++)
            {
                if (prioritiesSaveDataSlots[i].id == allPriorityObjects[j].id)
                {
                    allPriorityObjects[j].bAC.priorityManager.openedPriority.SetActive(true);
                    allPriorityObjects[j].bAC.price = prioritiesSaveDataSlots[i].remainPrice;
                }
            }
        }

    }

    void LoadData()
    {
        SaveLoadSystem.LoadAes<List<PrioritySaveDataSlot>>((data) =>
        {
            prioritiesSaveDataSlots = data;
        }, openedPrioritiesDataPath
        , (error) => { Debug.Log(error); }
        , (success) => { Debug.Log(success); });

    }

    public void DeleteOpenedPriorityFile()
    {
        SaveLoadSystem.DeleteFile(openedPrioritiesDataPath);
    }

}

[System.Serializable]
public class PriorityObjectSlot
{
    public int id;
    public BuyAreaController bAC;
    public PriorityObjectSlot(int id, BuyAreaController bAC)
    {
        this.id = id;
        this.bAC = bAC;
    }
}

[System.Serializable]
public class PrioritySaveDataSlot
{
    public int id;
    public int remainPrice;

    public PrioritySaveDataSlot(int id, int remainPrice)
    {
        this.id = id;
        this.remainPrice = remainPrice;
    }
}