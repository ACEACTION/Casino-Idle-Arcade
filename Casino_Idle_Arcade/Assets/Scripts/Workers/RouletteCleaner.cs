using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RouletteCleaner : Worker
{
    public bool isCleaning = true;
    public List<CasinoGame_ChipGame> casinoGames = new List<CasinoGame_ChipGame>();

    private void OnEnable()
    {
        WorkerManager.rouletteCleaners.Add(this);
        WorkerManager.AddAvaiableCasinoGamesToCleaner();

    }
     public override void Start()
    {
        agent.speed = workerData.moveSpeed;
        foreach (var casinoGame in casinoGames)
        {
            casinoGame.CallCleaner();
        }
    }
    private void Update()
    {
        if (destinationPoinst.Count != 0)
        {
            agent.SetDestination(destinationPoinst[0].transform.position);
            if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, destinationPoinst[0].transform.rotation, 0.1f);
                StartCoroutine(MoveToCleaningSpot());
                anim.SetBool("isWalking", false);
                anim.SetBool("isCleaning", true);

            }
            else
            {
                anim.SetBool("isCleaning", false);
                anim.SetBool("isWalking", true);
                agent.speed = workerData.moveSpeed;
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
    public void DisableCleaningAnim()
    {
        anim.SetBool("isCleaning", false);

    }

    IEnumerator MoveToCleaningSpot()
    {
        if (isCleaning)
        {
            isCleaning = false;
            agent.speed = 0;
            yield return new WaitForSeconds(destinationPoinst[0].transform.parent.GetComponent<Roulette>().cleaningCd);
            isCleaning = true;
            agent.speed = workerData.moveSpeed;

        }
    }
}
