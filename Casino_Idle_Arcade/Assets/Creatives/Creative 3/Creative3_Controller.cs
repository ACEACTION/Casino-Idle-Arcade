using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Creative3_Controller : MonoBehaviour
{
    public GameObject jackpotCustomerSpot1;
    public GameObject[] otherJackpotSpots;
    public GameObject zoomCam1;
    public GameObject zoomCam2;
    public GameObject normalCam;
   
    void Start()
    {
        jackpotCustomerSpot1.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            jackpotCustomerSpot1.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
            jackpotCustomerSpot1.SetActive(false);

        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject spot in otherJackpotSpots)
            {
                spot.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            zoomCam1.SetActive(false);
            zoomCam2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            zoomCam1.SetActive(true);
        }
               
    }
}
