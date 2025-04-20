

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;


public class Test : MonoBehaviour
{

    private void Start()
    {

    }

    public void BtnTest()
    {
        Application.OpenURL("myket://comment?id=ir.mservices.mybook");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyClass myObject = new MyClass();
            AnotherClass anotherObject = new AnotherClass();

            myObject.ValueChanged += anotherObject.HandleValueChanged; // Subscribe to the event

            myObject.MyValue = 10; // This will trigger the action in AnotherClass
        }
    }
}


public class MyClass
{
    private int myValue;
    public event Action<int> ValueChanged; // Event to notify the change

    public int MyValue
    {
        get { return myValue; }
        set
        {
            if (myValue != value)
            {
                myValue = value;
                OnValueChanged(myValue); // Trigger the event
            }
        }
    }

    protected virtual void OnValueChanged(int newValue)
    {
        ValueChanged?.Invoke(newValue); // Invoke the event
    }
}



// Class that handles the action
public class AnotherClass
{
    public void HandleValueChanged(int newValue)
    {
        // Perform the desired action with the new value
    }
}