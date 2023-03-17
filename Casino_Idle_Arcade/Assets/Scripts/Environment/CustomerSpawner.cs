using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    //variables
    [SerializeField] GameObject Customer; // the game object to spawn
    [SerializeField] float spawnTime = 1f; // time between each spawn
    public Transform spawnPoint; // the position where the object will be spawned
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnTime); // start spawning objects at intervals
    }

    void SpawnObject()
    {
        //check if line has empty slot


        //Getting from CustomerPool
        var cs = CustomerPool.instance.customerPool.Get();
        cs.transform.position = spawnPoint.position;
        
       //Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation); // create a new instance of the object at the spawn point
    }

}
