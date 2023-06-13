using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerModel : MonoBehaviour
{
    GameObject currentModel;
    [SerializeField] GameObject modelEff;
    [SerializeField] GameObject[] poorModels;
    [SerializeField] GameObject[] richModels;
    [HideInInspector] public int randomIndex;

    // virtual for creatives
    public virtual void OnEnable()
    {
        SetModelEffState(false);
        randomIndex = Random.Range(0, 2);
        if (randomIndex == 0) ActivePoorModel();
        else ActiveRichModel();
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

        
    public void SetModelEffState(bool state) => modelEff.SetActive(state);
}
