using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] GameObject[] cardModels;

    private void OnEnable()
    {
        int selectedIndex = Random.Range(0, cardModels.Length);
        cardModels[selectedIndex].SetActive(true);
        model = cardModels[selectedIndex];
    }

    private void OnDisable()
    {
        model.gameObject.SetActive(false);
    }
}
