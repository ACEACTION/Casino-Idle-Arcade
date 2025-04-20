using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipDesk : MonoBehaviour
{
    public Customer customer;
    public Transform customerSpot;
    public Transform chipPoint;
    public Transform moneySpawnPoint;
    public Transform chipSpawnPoint;
    public StackMoney stackMoney;

    [HideInInspector] public List<CasinoResource> releaseChipList;


    private void Start()
    {
        ChipDeskManager.chipDeskList.Add(this);
    }

    public void AddReleaseChipList(CasinoResource resource) => releaseChipList.Add(resource);


    public void ReleaseChips()
    {
        StartCoroutine(ReleaseChipsWithDelay());
    }

    IEnumerator ReleaseChipsWithDelay()
    {
/*        foreach (CasinoResource resource in releaseChipList)
        {
            yield return new WaitForSeconds(5f);
            resource.ReleaseResource();
        }*/

        yield return new WaitForSeconds(5f);
        for(int i = 0; i < releaseChipList.Count; i++)
        {
            releaseChipList[i].ReleaseResource();
        }
        releaseChipList.Clear();
    }

}
