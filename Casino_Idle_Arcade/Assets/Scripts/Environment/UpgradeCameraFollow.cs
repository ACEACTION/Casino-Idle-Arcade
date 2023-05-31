using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeCameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    public int counter = 0;
    [SerializeField] CinemachineVirtualCamera firstFollowCamera;

    [SerializeField] CinemachineVirtualCamera secondFollowCamera;
    [SerializeField] float stayCd;
    public List<GameObject> destinations = new List<GameObject>();

    public static UpgradeCameraFollow instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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
