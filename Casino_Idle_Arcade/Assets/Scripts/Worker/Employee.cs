using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EmployeeType
{
    RouletteDealer, Receptioner
}


public class Employee : MonoBehaviour
{
    public Animator anim;
    public virtual void ActiveActionAnim(bool state)
    {

    }

}
