using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class obstacleActivator : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshObstacle[] obstacles;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        obstacles = FindObjectsOfType<NavMeshObstacle>();
        foreach (NavMeshObstacle obstacle in obstacles)
        {
            obstacle.carving = false;
            yield return new WaitForEndOfFrame();
            obstacle.carving = true;
        }


    }
}
