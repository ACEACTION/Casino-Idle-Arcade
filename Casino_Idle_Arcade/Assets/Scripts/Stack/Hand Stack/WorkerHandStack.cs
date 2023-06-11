using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHandStack : HandStack
{    
    public override void AddStackResourceProcess()
    {
        base.AddStackResourceProcess();

    }

    public override void RemoveFromStackWithCd()
    {
        base.RemoveFromStackWithCd();
    }

    public override void RemoveFromStackProcess(CasinoResource resource)
    {
        base.RemoveFromStackProcess(resource);
        MaxStackText.Instance.SetTextState(false);
    }

    public override void RemoveFromStack()
    {
        base.RemoveFromStack();
    }
}
