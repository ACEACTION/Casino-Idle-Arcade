using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CustomerMovement : Customer
{

    [SerializeField] NavMeshAgent agent;

    public Transform destination;


    private void Update()
    {
        if (destination != null)
        {
            agent.SetDestination(destination.position);

            if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
            {
                anim.SetBool("idle", true);
                anim.SetBool("isWalking", false);
            }
        }
    }


    public void SetMove(Transform ts)
    {
        anim.SetBool("isWalking", true);
        destination = ts;

    }

}
