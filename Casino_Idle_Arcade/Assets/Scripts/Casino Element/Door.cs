using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] float cd;
    private void Update()
    {
        cd -= Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (cd < 0)
        {
            cd = 4f;
            AudioSourceManager.Instance.PlayDoorSound();
        }
    }
}
