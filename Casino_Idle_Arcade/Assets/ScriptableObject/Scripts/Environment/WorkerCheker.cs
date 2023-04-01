using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorkerCheker : MonoBehaviour
{
    [SerializeField] WorkerType workerType;
    public Worker worker;
    public bool isWorkerAvailable;
    public bool isPlayerAvailable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(workerType.ToString()))
        {
            isWorkerAvailable = true;
            worker = other.gameObject.GetComponent<Worker>();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAvailable = false;
        }
    }
}
