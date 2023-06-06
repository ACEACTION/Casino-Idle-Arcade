using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RouletteCleaner : Worker
{
    bool canFollow = true;
    public bool playerMaxStackTxtState;
    public bool getPlayerMaxStackTxtState;
    [SerializeField] float camStayCd;
    [SerializeField] Vector3 offSet;
    public bool isCleaning = true;
    public List<CasinoGame_ChipGame> casinoGames = new List<CasinoGame_ChipGame>();
    private void OnEnable()
    {
        WorkerManager.rouletteCleaners.Add(this);
        WorkerManager.AddAvaiableCasinoGamesToCleaner();

    }
     public override void Start()
    {
        ArrivedToFirstPosition();
        CameraFollow.instance.SetDynamicFollow_BuyWorker(transform);

        transform.position = spawnPoint.position;
        agent.speed = workerData.moveSpeed;
    
        foreach (var casinoGame in casinoGames)
        {
            casinoGame.CallCleaner();
        }
    }

    public override void ArrivedToFirstPosition()
    {
        base.ArrivedToFirstPosition();
        anim.SetBool("isWalking", true);
        agent.SetDestination(afterSpawnTransform.position);

    }
    private void Update()
    {
        if (!canWork)
        {
            //if (canFollow)
            //{
            //    CameraFollow.instance.firstFollowCamera.gameObject.SetActive(true);
            //    CameraFollow.instance.CamFollowDynamic(transform);

            //    if (!getPlayerMaxStackTxtState)
            //    {
            //        getPlayerMaxStackTxtState = true;
            //        playerMaxStackTxtState = MaxStackText.Instance.gameObject.activeSelf;
            //    }

            //    MaxStackText.Instance.gameObject.SetActive(false);

            //    camStayCd -= Time.deltaTime;
            //    if (camStayCd <= 0)
            //    {
            //        MaxStackText.Instance.gameObject.SetActive(playerMaxStackTxtState);
            //        canFollow = false;
            //        CameraFollow.instance.firstFollowCamera.gameObject.SetActive(false);
            //    }
            //}
            //we have to wait till worker arrives to first position
            if (Vector3.Distance(transform.position, afterSpawnTransform.position) <= agent.stoppingDistance)
            {
                //worker arrives to first position
                canWork = true;
                anim.SetBool("isWalking", false);
            }

        }
        if (canWork)
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
                if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                {
                    anim.SetBool("isWalking", false);
                }
                else
                {
                    anim.SetBool("isWalking", true);

                }
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
