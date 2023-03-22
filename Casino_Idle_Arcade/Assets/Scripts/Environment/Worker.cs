using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WorkerType
{
    RouletteDealer, Receptioner
}
public class Worker : MonoBehaviour
{
    public Animator anim;

    public virtual void ActiveActionAnim(bool state)
    {

    }
}
