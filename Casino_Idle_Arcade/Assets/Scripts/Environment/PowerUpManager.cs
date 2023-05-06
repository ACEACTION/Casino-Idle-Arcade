using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpData")]
public class PowerUpManager : ScriptableObject
{
    public float lifeTime;
    public float lifeTimeDuration;
    //this is gonna be place holder for next time we have adv [serializefield] tablighe tabligh;
    [SerializeField] PowerUpData powerUpData;
    [SerializeField] ParticleSystem powerUpVfx;

    [SerializeField] GameObject powerUps;

    public float spawnRange;
    [SerializeField] Transform[] spawnPoints;

}
