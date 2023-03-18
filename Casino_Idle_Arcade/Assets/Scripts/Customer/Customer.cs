using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    inLine, firstCounter, movingIn, leaving, playingGame
}
public class Customer : MonoBehaviour
{
    public Animator anim;
    public CustomerState csState;
    public CustomerData stats;


    public void Leave()
    {

    }
}
