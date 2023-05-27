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

        AudioSourceManager.Instance.PlayPoPSfx();
        StartCoroutine(SetVibrate());
        
        // vibration

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

        // vibration

        MaxStackText.Instance.SetTextState(false);
    }

    public override void RemoveFromStack()
    {
        base.RemoveFromStack();
        if (trash)
        {
            CasinoResource resource = stackList[stackList.Count - 1];
            resource.transform.DOJump(trash.position, 3, 1, .2f)
                .OnComplete(() =>
                {
                    resource.ReleaseResource();
                });

            chipList.Remove(resource);
            vMachineList.Remove(resource);
            RemoveFromStackProcess(resource);
        }
    }


    IEnumerator SetVibrate()
    {
        Vibrator.Vibrate(5000);
        yield return new WaitForSeconds(0);
        Vibrator.Cancel();
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
