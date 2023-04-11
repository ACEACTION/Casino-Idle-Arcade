using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectWave : MonoBehaviour
{
    [SerializeField] ParticleSystem waveEffec;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waveEffec.gameObject.SetActive(true);
            waveEffec.Play();
        }
    }
}
