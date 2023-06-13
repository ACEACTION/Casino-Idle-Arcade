using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creative2_WorkerHandStack : WorkerHandStack
{
    public Transform firstStack1;
    public Transform firstStack2;
    int ascendingCounter = 0;

    public override void AddStackResourceProcess()
    {
        base.AddStackResourceProcess();
        
        if (GetStackCount() >= data.maxStackCount / 2)
        {
            SetFirstStack(firstStack2);
           
        }
    }

    public override void RemoveFromStack()
    {
        if (GetStackCount() < data.maxStackCount / 2)
        {
            SetFirstStack(firstStack1);
        }

        base.RemoveFromStack();
    }



}
