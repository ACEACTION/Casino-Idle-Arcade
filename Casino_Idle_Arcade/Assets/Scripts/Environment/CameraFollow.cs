using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] Vector3 offset;
    [SerializeField] CinemachineVirtualCamera normalCam;
    int counter = 0;
    public CinemachineVirtualCamera firstFollowCamera;

    [SerializeField] CinemachineVirtualCamera secondFollowCamera;    
    [SerializeField] float stayCd;

    public List<GameObject> destinations = new List<GameObject>();

    [Header("Follow worker")]
    [SerializeField] float maxFollowWorkerCd;
    public float followWorkerCd;
    bool dynamicFollow;
    Transform worker;
    public static CameraFollow instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        followWorkerCd = maxFollowWorkerCd;
    }


    private void Update()
    {
        if (dynamicFollow)
        {
            firstFollowCamera.gameObject.SetActive(true);
            CamFollowDynamic(worker);
            followWorkerCd -= Time.deltaTime;
            if (followWorkerCd <= 0)
            {
                firstFollowCamera.gameObject.SetActive(false);
                dynamicFollow = false;
                followWorkerCd = maxFollowWorkerCd;
                PlayerMovements.Instance.canMove = true;
                MainUpgradePanel.Instance.gameObject.SetActive(true);               
            }
        }
    }


    void SetDynamicFollow(Transform obj)
    {
        secondFollowCamera.gameObject.SetActive(false);
        firstFollowCamera.gameObject.SetActive(true);
        dynamicFollow = true;
        worker = obj;
        PlayerMovements.Instance.canMove = false;
        Joystick.Instance.background.gameObject.SetActive(false);
    }

    public void CamFollowDynamic(Transform obj) => firstFollowCamera.transform.position = obj.position + offset;

    public void SetDynamicFollow_UpgradeWorker(Transform obj, WorkerUpgradeEffect upgradeEff, string upgradeMsg)
    {
        SetDynamicFollow(obj);
        StartCoroutine(ShowUpgradeWorkerMsg(upgradeEff, upgradeMsg));
    }

    IEnumerator ShowUpgradeWorkerMsg(WorkerUpgradeEffect upgradeEff, string msg)
    {
        MainUpgradePanel.Instance.gameObject.SetActive(false);
        float delay = maxFollowWorkerCd / 2;
        yield return new WaitForSeconds(delay);
        upgradeEff.UpgradeEffectProcess(msg);
    }

    public void DoCoroutine()
    {
        StartCoroutine(FollowNewAreas());
    }

    public IEnumerator FollowNewAreas()
    {
        yield return new WaitForSeconds(.1f);
            PlayerMovements.Instance.canMove = false;

            Joystick.Instance.ResetJoystick();
            Joystick.Instance.gameObject.SetActive(false);

        if (destinations.Count == 1) counter--;
        for (int i = 0;  counter < (destinations.Count/2); i+=2)
            {

            firstFollowCamera.transform.position = destinations[i].transform.position + offset;
                firstFollowCamera.transform.gameObject.SetActive(true);
                secondFollowCamera.gameObject.SetActive(false);

           yield return new WaitForSeconds(stayCd);
            if (i + 1 < destinations.Count && destinations[i + 1] != null)
            {
                secondFollowCamera.transform.position = destinations[i+1].transform.position + offset;

                secondFollowCamera.gameObject.SetActive(true);

            }
            firstFollowCamera.transform.gameObject.SetActive(false);


            counter++;

            yield return new WaitForSeconds(stayCd);
            }

            secondFollowCamera.gameObject.SetActive(false);
            Joystick.Instance.gameObject.SetActive(true);
            Joystick.Instance.ResetJoystick();
            PlayerMovements.Instance.canMove = true;
            counter = 0;
            destinations.Clear();



    }
}
