using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class SaveLoad_Workers : MonoBehaviour
{
    const string workerSaveDatasPath = "workerData";
    [SerializeField] List<WorkerObj> workerObjs = new List<WorkerObj>();
    [SerializeField] List<int> workersData = new List<int>();

    public static SaveLoad_Workers Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        LoadWorkes();
    }

    void LoadWorkes()
    {
        LoadData();

        for (int i = 0; i < workersData.Count; i++)
        {
            for (int j = 0; j < workerObjs.Count; j++)
            {
                if (workersData[i] == workerObjs[j].id)
                {
                    workerObjs[j].worker.camFollow = false;
                    workerObjs[j].worker.gameObject.SetActive(true);
                    workerObjs[j].bAC.priorityManager.openedPriority.SetActive(true);
                    workerObjs[j].bAC.gameObject.SetActive(false);
                    workerObjs[j].priorityUi.ActivePriorityObjs();
                }
            }
        }

    }

    public void SaveWorker(Worker worker)
    {
        int workerId = GetIdByWorker(worker);

        if (workerId == -1)
        {
            print("the worker is not found");
            return;
        }

        if (!workersData.Contains(workerId))
        {
            workersData.Add(workerId);
            SaveData();
        }

    }

    void SaveData()
    {
        SaveLoadSystem.SaveAes(workersData, workerSaveDatasPath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });
    }

    void LoadData()
    {
        SaveLoadSystem.LoadAes<List<int>>((data) =>
        {
            workersData = data;
        }, workerSaveDatasPath
           , (error) => { Debug.Log(error); }
           , (success) => { Debug.Log(success); });
    }

    int GetIdByWorker(Worker worker)
    {
        for (int i = 0; i < workerObjs.Count; i++)
        {
            if (workerObjs[i].worker == worker)
            {
                return workerObjs[i].id; 
            }
        }
        return -1;
    }

    public void DeleteWorkersDataFile() => SaveLoadSystem.DeleteFile(workerSaveDatasPath);

}

[System.Serializable]
public class WorkerObj
{
    public int id;
    public Worker worker;
    public BuyAreaController bAC;
    public PriorityManagerUI priorityUi;
}

[System.Serializable]
public class WorkerSaveData
{
    public int id;
}