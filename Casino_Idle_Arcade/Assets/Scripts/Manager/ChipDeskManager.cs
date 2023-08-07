using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChipDeskManager
{
    public static List<ChipDesk> chipDeskList = new List<ChipDesk>();


    public static ChipDesk FindNearestChipDesk(Transform customer)
    {
        ChipDesk nearestDesk = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = customer.position;

        foreach (ChipDesk desk in chipDeskList)
        {
            float distance = Vector3.Distance(desk.transform.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestDesk = desk; 
            }
        }

        return nearestDesk;
    }

    public static void ResetData()
    {
        chipDeskList.Clear();
    }

}
