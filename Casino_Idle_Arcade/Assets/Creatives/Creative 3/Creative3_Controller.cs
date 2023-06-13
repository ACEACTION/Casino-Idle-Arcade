using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creative3_Controller : MonoBehaviour
{
    public GameObject jackpotCustomerSpot1;
    public GameObject zoomCam;
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            zoomCam.SetActive(false);
            normalCam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            zoomCam.SetActive(true);
            normalCam.SetActive(false);
        }

    }
}
