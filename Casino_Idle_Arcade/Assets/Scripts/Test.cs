using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public float areaCost = 500;
    public bool player;
    public float maxCooldown;
    float cooldown = 1.0f;
    bool areaBought = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = false;
        }
    }

    private void Start()
    {
        cooldown = maxCooldown;
    }

    public int a;
    private void Update()
    {
        if (player)
        {
            areaCost = Mathf.Lerp(areaCost, 0, cooldown);
            a = Mathf.FloorToInt(areaCost);
        }
    }
}
