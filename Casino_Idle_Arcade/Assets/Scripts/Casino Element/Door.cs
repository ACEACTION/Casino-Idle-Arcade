using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        AudioSourceManager.Instance.PlayDoorSound();

    }
}
