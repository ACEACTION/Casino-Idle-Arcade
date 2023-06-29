using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class carpetScaler : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1.3f, 0.5f);

        }
    }
}
