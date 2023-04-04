using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineManager : MonoBehaviour
{
    public CinemachineVirtualCamera normalCamera;
    public CinemachineVirtualCamera zoomCam;

    public bool isNormalCam = false;
    public static CinemachineManager instance;


    private void Awake()
    {
        if (instance == null) instance = this;
    }


    public void ChangeCam()
    {
        if (isNormalCam)
        {
            normalCamera.Priority = 1;
            zoomCam.Priority = 0;
        }
        else
        {
            normalCamera.Priority = 0;
            zoomCam.Priority = 1;
        }
        isNormalCam = !isNormalCam;

    }
}
