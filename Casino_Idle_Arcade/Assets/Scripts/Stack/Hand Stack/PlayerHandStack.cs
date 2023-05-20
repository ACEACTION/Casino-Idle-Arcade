using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHandStack : HandStack
{    
    Transform trash;


    public override void AddStackResourceProcess()
    {
        base.AddStackResourceProcess();

        if (StackIsMax())
        {
            MaxStackText.Instance.SetTextState(true);
        }
    }

    public override void RemoveFromStackWithCd()
    {
        base.RemoveFromStackWithCd();
        if (trash && CanRemoveStack())
        {
            RemoveFromStackWithCdProcess();
        }
    }

    public override void RemoveFromStackProcess(CasinoResource resource)
    {
        base.RemoveFromStackProcess(resource);
        MaxStackText.Instance.SetTextState(false);
    }

    public override void RemoveFromStack()
    {
        base.RemoveFromStack();
        if (trash)
        {
            CasinoResource resource = stackList[stackList.Count - 1];
            resource.transform.DOJump(trash.position, 3, 1, .6f)
                .OnComplete(() =>
                {
                    resource.ReleaseResource();
                });

            chipList.Remove(resource);
            vMachineList.Remove(resource);
            RemoveFromStackProcess(resource);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Trash"))
        {
            trash = other.transform;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.CompareTag("Trash"))
        {
            trash = null;
        }
    }


}
