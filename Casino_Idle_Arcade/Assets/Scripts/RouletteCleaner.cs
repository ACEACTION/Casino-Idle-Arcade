using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RouletteCleaner : Cleaner
{
    [SerializeField] NavMeshAgent agent;
    public bool isCleaning = true;
    public List<Roulette> roulettes = new List<Roulette>();

    private void OnEnable()
    {

    }
    private void Start()
    {
        agent.speed = workerData.moveSpeed;
        WorkerManager.rouletteCleaners.Add(this);
        WorkerManager.AddAvaiableRouletteToCleaner();
    }
    private void Update()
    {
        if (cleaningSpot.Count != 0)
        {
            agent.SetDestination(cleaningSpot[0].transform.position);
            if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, cleaningSpot[0].transform.rotation, 0.1f);
                StartCoroutine(MoveToCleaningSpot());
                anim.SetBool("isWalking", false);

            }
            else
            {
                anim.SetBool("isWalking", true);

            }



        }
        else
        {
            agent.SetDestination(sweeperSpot.position);
            if(Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);

            }
        }
    }


    IEnumerator MoveToCleaningSpot()
    {
        if (isCleaning)
        {

            isCleaning = false;
            agent.speed = 0;
            yield return new WaitForSeconds(cleaningSpot[0].GetComponent<Roulette>().cleaningCd);
            isCleaning = true;
            agent.speed = workerData.moveSpeed;


        }
    }
}
