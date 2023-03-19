using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CustomerMovement : Customer
{
    Transform dir;
    [SerializeField] NavMeshAgent agent;

    public Transform destination;


    private void Update()
    {
        dir = destination;
        if (destination != null)
        {
            agent.SetDestination(destination.position);
            if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
            {

                transform.rotation = Quaternion.Slerp(transform.rotation, dir.rotation, 0.1f);
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
