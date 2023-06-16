using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnableNavmeshObstacle : MonoBehaviour
{
    [SerializeField] NavMeshObstacle obstacle;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        obstacle.carving = true;
    }
}
