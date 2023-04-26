using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteWorkerCheker : WorkerCheker
{
    public Roulette roullete;
    public bool isCleanerAvailabe;
    public bool canChangeCamera;


    private void OnTriggerEnter(Collider other)
    {
        if(roullete.cleaner != null)
        {
            if (other.gameObject.Equals(roullete.cleaner.gameObject))
            {
                isCleanerAvailabe = true;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            canChangeCamera = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roullete.cleaner != null)
        {
            if (other.gameObject.Equals(roullete.cleaner.gameObject))
            {
                isCleanerAvailabe = false;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            CinemachineManager.instance.ZoomOut();
            if (CinemachineManager.instance.isNormalCam)
                CinemachineManager.instance.ChangeCam();

        }
    }

}
