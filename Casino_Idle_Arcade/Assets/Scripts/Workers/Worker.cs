using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public CinemachineVirtualCamera flCam;
    [HideInInspector] public bool camFollow = true;


    public NavMeshAgent agent;
    public Transform afterSpawnTransform;
    public Transform spawnPoint;
    public bool canWork = false;

    public WorkerData workerData;
    public List<Transform> destinationPoinst = new List<Transform>();
    public Transform sweeperSpot;
    public int capacity;
    public int capacityAmount;
    public Animator anim;

    //public BuyAreaController buyAreaController;


    public virtual void Start()
    {
        SetMoveSpeed();
        SaveLoad_Workers.Instance.SaveWorker(this);
    }

    public virtual void ArrivedToFirstPosition()
    {

    }
    public void SetMoveSpeed()
    {
        agent.speed = workerData.moveSpeed;
    }


}
