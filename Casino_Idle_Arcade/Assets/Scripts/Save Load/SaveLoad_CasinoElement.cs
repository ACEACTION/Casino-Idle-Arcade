
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class SaveLoad_CasinoElement : MonoBehaviour
{
    const string casinoElementSaveDatasPath = "casinoElementSaveDatas";

    [SerializeField] List<CasinoElementId> casinoElementsId;
    [SerializeField] List<CasinoElementSaveData> elementsSaveDatas;



    public static SaveLoad_CasinoElement Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < casinoElementsId.Count; i++)
        {
            casinoElementsId[i].elementId = i;
        }

        if (!GameManager.isCompleteTutorial)
            DeleteCasinoElementDataFile();

        StartCoroutine(LoadCasinoElements());
    }



    IEnumerator LoadCasinoElements()
    {
        LoadData();

        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < elementsSaveDatas.Count; i++)
        {
            for (int j = 0; j < casinoElementsId.Count; j++)
            {
                if (elementsSaveDatas[i].elementId == casinoElementsId[j].elementId)
                {
                    casinoElementsId[j].element.upgradeIndex = elementsSaveDatas[i].upgradeIndex;
                    casinoElementsId[j].element.gameObject.SetActive(true);
                    casinoElementsId[j].element.transform.parent.gameObject.SetActive(true);
                    casinoElementsId[j].element.buyAreaController.gameObject.SetActive(false);
                    yield return new WaitForSeconds(.2f);
                    casinoElementsId[j].element.GetStackMoney().InitStackMoney(elementsSaveDatas[i].stackMoneyAmount
                        , MoneyType.baccaratMoney);
                }
            }
        }

    }

    void LoadData()
    {
        SaveLoadSystem.LoadAes<List<CasinoElementSaveData>>((data) =>
        {
            elementsSaveDatas = data;
        }, casinoElementSaveDatasPath
        , (error) => { Debug.Log(error); }
        , (success) =>
        { 
            //Debug.Log(success); 
        });
    }

    public void AddItemToElementsSaveDatas(CasinoElement element)
    {
        int id = GetId(element);

        if (id == -1)
        {
            Debug.Log("the element doesnt add to list");
            return;
        }

        foreach (CasinoElementSaveData data in elementsSaveDatas)
        {
            if (data.elementId == id)
            {
                data.upgradeIndex = element.upgradeIndex;

                SaveElementData();
                return;
            }
        }

        elementsSaveDatas.Add(new CasinoElementSaveData(id, element.upgradeIndex));
        SaveElementData();
    }

    void SaveElementData()
    {
        SaveLoadSystem.SaveAes(elementsSaveDatas, casinoElementSaveDatasPath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
            //Debug.Log(success);
        });
    }


    int GetId(CasinoElement element)
    {

        for (int i = 0; i < casinoElementsId.Count; i++)
        {
            if (casinoElementsId[i].element == element)
                return casinoElementsId[i].elementId;
        }

        return -1;
    }

    public void SaveStackMoneyAmount()
    {
        for (int i = 0; i < elementsSaveDatas.Count; i++)
        {
            for (int j = 0; j < casinoElementsId.Count; j++)
            {
                if (elementsSaveDatas[i].elementId == casinoElementsId[j].elementId)
                {
                    elementsSaveDatas[i].stackMoneyAmount = casinoElementsId[j].element.GetStackMoney().StackCounter;
                    break;
                }
            }
        }
        SaveElementData();
    }


    public void DeleteCasinoElementDataFile()
    {
        elementsSaveDatas.Clear();
        SaveLoadSystem.DeleteFile(casinoElementSaveDatasPath);
    }

}

[System.Serializable]
public class CasinoElementId
{
    public int elementId;
    public CasinoElement element;
}

[System.Serializable]
public class CasinoElementSaveData
{
    public int elementId;
    public int upgradeIndex;
    public int stackMoneyAmount;

    public CasinoElementSaveData(int elementId, int upgradeIndex)
    {
        this.elementId = elementId;
        this.upgradeIndex = upgradeIndex;
    }
}