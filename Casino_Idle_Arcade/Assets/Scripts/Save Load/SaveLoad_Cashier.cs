using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmirSaveLoadSystem;

public class SaveLoad_Cashier : MonoBehaviour
{
    const string cashierDataPath = "CashierDeskData";

    [SerializeField] List<CashierObjSlot> cashierObjSlots = new List<CashierObjSlot>();
    [SerializeField] List<CashierDataSlot> cashierDataSlots = new List<CashierDataSlot>();
    

    public static SaveLoad_Cashier Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
       StartCoroutine(LoadCashierData());
    }

    public void SaveCashierDesk(BuyAreaController bAC)
    {
        int cashierId = GetIdByBuyAreaController(bAC);
        if (cashierId == -1)
        {
            print("The buy area controller is not found");
            return;
        }


        for (int i = 0; i < cashierDataSlots.Count; i++)
        {
            if (cashierId == cashierDataSlots[i].id)
            {
                cashierDataSlots[i].cashierIsOpen = true;
                SaveData();
                return;
            }
        }

        cashierDataSlots.Add(new CashierDataSlot(cashierId, true, false));
        SaveData();
    }

    public void SaveReception(UpgradeCashierDesk upgradeArea)
    {
        int cashierId = GetIdByUpgradeArea(upgradeArea);
        if (cashierId == -1)
        {
            print("The buy area controller is not found");
            return;
        }


        for (int i = 0; i < cashierDataSlots.Count; i++)
        {
            if (cashierId == cashierDataSlots[i].id)
            {
                cashierDataSlots[i].receptionIsOpen = true;
                SaveData();
                return;
            }
        }

        cashierDataSlots.Add(new CashierDataSlot(cashierId, true, true));
        SaveData();
    }

    IEnumerator LoadCashierData()
    {
        LoadData();

        for (int i = 0; i < cashierDataSlots.Count; i++)
        {
            for (int j = 0; j < cashierObjSlots.Count; j++)
            {
                if (cashierDataSlots[i].id == cashierObjSlots[j].id)
                {
                    if (cashierDataSlots[i].cashierIsOpen)
                    {
                        cashierObjSlots[j].buyArea.buyedElement.SetActive(true);
                        cashierObjSlots[j].buyArea.priorityManager.openedPriority.SetActive(true);
                        cashierObjSlots[j].buyArea.gameObject.SetActive(false);


                        if (cashierDataSlots[i].receptionIsOpen)
                        {
                            cashierObjSlots[j].upgradeArea.DestroyAreas();
                            cashierObjSlots[j].upgradeArea.gameObject.SetActive(false);
                            cashierObjSlots[j].upgradeArea.bAController.buyedElement.SetActive(true);
                        }

                        yield return new WaitForSeconds(.2f);
                        cashierObjSlots[j].stackMoney.InitStackMoney(cashierDataSlots[i].stackMoneyAmount
                        , MoneyType.receptionMoney);

                    }
                }
            }
        }

    }

    void SaveData()
    {
        SaveLoadSystem.SaveAes(cashierDataSlots, cashierDataPath,
            (error) => {
                Debug.Log(error);
            },
            (success) => {
                Debug.Log(success);
            });
    }

    void LoadData()
    {
        SaveLoadSystem.LoadAes<List<CashierDataSlot>>((data) =>
        {
            cashierDataSlots = data;
        }, cashierDataPath
           , (error) => { Debug.Log(error); }
           , (success) => { Debug.Log(success); });
    }

    int GetIdByBuyAreaController(BuyAreaController bAC)
    {
        for (int i = 0; i < cashierObjSlots.Count; i++)
        {
            if (bAC == cashierObjSlots[i].buyArea)
                return cashierObjSlots[i].id;
        }
        return -1;
    }

    int GetIdByUpgradeArea(UpgradeCashierDesk upgradeArea)
    {
        for (int i = 0; i < cashierObjSlots.Count; i++)
        {
            if (upgradeArea == cashierObjSlots[i].upgradeArea)
                return cashierObjSlots[i].id;
        }
        return -1;
    }

    public void DeleteCashierData() => SaveLoadSystem.DeleteFile(cashierDataPath);


    private void OnApplicationQuit()
    {
        for (int i = 0; i < cashierDataSlots.Count; i++)
        {
            for (int j = 0; j < cashierObjSlots.Count; j++)
            {
                if (cashierDataSlots[i].id == cashierObjSlots[j].id)
                {
                    cashierDataSlots[i].stackMoneyAmount = cashierObjSlots[j].stackMoney.stackCounter;
                    break;
                }
            }
        }

        SaveData();

    }

}

[System.Serializable]
public class CashierObjSlot
{
    public int id;
    public BuyAreaController buyArea;
    public UpgradeCashierDesk upgradeArea;
    public StackMoney stackMoney;
}

[System.Serializable]
public class CashierDataSlot
{
    public int id;
    public bool cashierIsOpen;
    public bool receptionIsOpen;
    public int stackMoneyAmount;

    public CashierDataSlot(int id, bool cashierIsOpen, bool receptionIsOpen)
    {
        this.id = id;
        this.cashierIsOpen = cashierIsOpen;
        this.receptionIsOpen = receptionIsOpen;
    }

}
