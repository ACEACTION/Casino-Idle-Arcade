using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoElementSpotController : MonoBehaviour
{
    [SerializeField] CasinoElement casinoElement;
    public List<CasinoElementSpot> elementSpots = new List<CasinoElementSpot>();
    
    private void Start()
    {
        
    }

    public void ResetElementSpot()
    {
        // when upgrade element, if customer is in spot, spot doesnt work
        // so we deactive/active to work
        foreach (CasinoElementSpot spot in elementSpots)
        {
            spot.gameObject.SetActive(false);
            spot.gameObject.SetActive(true);
        }
    }

}
