using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float goToPlayerTime;
    bool goToPlayer;


    private void Update()
    {
        if (goToPlayer)
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                PlayerMovements.Instance.transform.position + new Vector3(0, 1, 0), 
                goToPlayerTime * Time.deltaTime);

            if (Vector3.Distance(transform.position, 
                PlayerMovements.Instance.transform.position + new Vector3(0, 1, 0)) < .1f)
            {
                goToPlayer = false;
                StackMoneyMaker.Instance.OnReleaseMoney(this);
            }
        }
    }

    public void SetGoToPlayer()
    {
        goToPlayer = true;
    }

}
