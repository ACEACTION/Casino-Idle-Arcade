using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHandStack : HandStack
{
    int ascendingCounter = 0;

    public override void AddStackResourceProcess()
    {
        base.AddStackResourceProcess();
        PlayPopSfx();

    }

    public override void RemoveFromStackWithCd()
    {
        base.RemoveFromStackWithCd();

    }

    public override void RemoveFromStackProcess(CasinoResource resource)
    {
        base.RemoveFromStackProcess(resource);
        MaxStackText.Instance.SetTextState(false);
        PlayPopSfx();

    }

    public override void RemoveFromStack()
    {
        base.RemoveFromStack();
    }

    //creative 2
    public void PlayPopSfx()
    {
        AudioSourceManager.Instance.PlayPoPSfx(ascendingCounter);
        ascendingCounter++;
        if (ascendingCounter == 7)
        {
            ascendingCounter = 0;
        }
    }
}
