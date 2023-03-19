using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTransformPositions : MonoBehaviour
{
    [SerializeField] Table parentTable;

    int indexOfThisPlace;

    private void Start()
    {
        indexOfThisPlace = parentTable.customerSpot.IndexOf(this.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(parentTable.customerList[indexOfThisPlace].gameObject))
        {
            parentTable.customerList.Remove(parentTable.customerList[indexOfThisPlace]);
            parentTable.hasEmptySlots = true;
        }
    }
}
