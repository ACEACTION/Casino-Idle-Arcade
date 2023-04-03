using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTransformPositions : MonoBehaviour
{
    [SerializeField] Table parentTable;

    public int IndexOfThisPlace;
    private void Start()
    {
        IndexOfThisPlace = parentTable.customerSpot.IndexOf(this.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentTable.customersInPlace[IndexOfThisPlace] != null)
        {
            if (other.gameObject.Equals(parentTable.customersInPlace[IndexOfThisPlace].gameObject))
            {

                parentTable.customersInPlaceCount++;

                if (parentTable.customersInPlaceCount == parentTable.maximumCapacity)
                {
                    parentTable.readyToPlay = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (parentTable.customersInPlace[IndexOfThisPlace] != null)
        {

            if (other.gameObject.Equals(parentTable.customersInPlace[IndexOfThisPlace].gameObject))
            {

                parentTable.customerList.Remove(parentTable.customersInPlace[IndexOfThisPlace]);
                parentTable.customersInPlaceCount = 0;
                parentTable.customersInPlace[IndexOfThisPlace] = null;
                parentTable.readyToPlay = false;
                parentTable.hasEmptySlots = true;

            }
        }
    }
}
