using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerModel : MonoBehaviour
{
    [SerializeField] GameObject normalModel;
    GameObject currentModel;
    [SerializeField] GameObject modelEff;
    [SerializeField] GameObject[] poorModels;
    [SerializeField] GameObject[] richModels;
    //[HideInInspector] public int randomIndex;

    // virtual for creatives
    public virtual void OnEnable()
    {
        SetModelEffState(false);
        currentModel = normalModel;
    }

    public void ActivePoorModel()
    {
        ActiveModel(poorModels[Random.Range(0, poorModels.Length)]);        
    }

    public void ActiveRichModel()
    {
        ActiveModel(richModels[Random.Range(0, richModels.Length)]);
    }

    void ActiveModel(GameObject model)
    {
        currentModel?.SetActive(false);
        currentModel = model;
        currentModel.SetActive(true);
    }

    public void ResetModel()
    {
        currentModel.SetActive(false);
        normalModel.SetActive(true);
    }


    public void SetModelEffState(bool state) => modelEff.SetActive(state);
}
