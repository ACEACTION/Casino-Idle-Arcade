using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public NavMeshAgent agent;
    public WorkerData workerData;
    public List<Transform> destinationPoinst = new List<Transform>();
    public Transform sweeperSpot;
    public int capacity;
    public int capacityAmount;
    public Animator anim;

    public virtual void Start()
    {
        SetMoveSpeed();
    }

    public void SetMoveSpeed()
    {
        agent.speed = workerData.moveSpeed;
    }


}
