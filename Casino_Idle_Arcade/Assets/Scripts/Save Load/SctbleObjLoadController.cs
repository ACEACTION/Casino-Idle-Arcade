using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SctbleObjLoadController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerHandStackData playerStack; 
    [SerializeField] PlayerData playerData;

    [Header("Cleaner")]
    [SerializeField] WorkerData cleanerData1;
    [SerializeField] WorkerData cleanerData2;

    [Header("Chip Deliver")]
    [SerializeField] HandStackData chipDeliverStackData1;
    [SerializeField] WorkerData chipDeliverData1;
    [SerializeField] HandStackData chipDeliverStackData2;
    [SerializeField] WorkerData chipDeliverData2;


    private void Start()
    {
        LoadUpgradeData();
    }

    void LoadUpgradeData()
    {
        playerStack.LoadData();
        playerData.LoadData();
        cleanerData1.LoadData();
        cleanerData2.LoadData();
        chipDeliverStackData1.LoadData();
        chipDeliverData1.LoadData();
        chipDeliverStackData2.LoadData();
        chipDeliverData2.LoadData();
    }



}
