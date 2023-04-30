using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public WorkerData workerData;
    public List<Transform> destinationPoinst = new List<Transform>();
    public Transform sweeperSpot;
    public int capacity;
    public int capacityAmount;
    public Animator anim;

}
