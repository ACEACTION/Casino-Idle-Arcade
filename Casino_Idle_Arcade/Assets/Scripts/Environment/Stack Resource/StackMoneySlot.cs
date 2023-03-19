using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMoneySlot : MonoBehaviour
{
    [SerializeField] bool isFull;
    public GameObject resource;

    public List<GameObject> resources;

    public bool IsFull() => isFull;
    public void SetFullState(bool state) => isFull = state;
    public void SetResource(GameObject r) => resource = r;
    public GameObject GetResource() => resource;

}
