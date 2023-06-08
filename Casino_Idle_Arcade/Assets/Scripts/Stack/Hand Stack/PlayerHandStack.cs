using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHandStack : HandStack
{    
    Transform trash;
    int ascendingCounter = 0;
    int descendingCounter = 7;


    private void Start()
    {
        Vibration.Init();
    }

    public override void AddStackResourceProcess()
    {
        base.AddStackResourceProcess();

        PlayStackSfx();

        // StartCoroutine(SetVibrate());

        // vibration
        TapVibrateCustom();

        // set animation
        anim.SetBool("isCarry", true);

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
        TapVibrateCustom();

        PlayStackSfx();


        // set animation
        if (StackIsEmpty())
            anim.SetBool("isCarry", false);


        MaxStackText.Instance.SetTextState(false);
    }

    public override void RemoveFromStack()
    {
        base.RemoveFromStack();
        if (trash)
        {
            CasinoResource resource = stackList[stackList.Count - 1];
            resource.transform.DOJump(trash.position, 3, 1, .1f)
                .OnComplete(() =>
                {
                    resource.ReleaseResource();
                });

            chipList.Remove(resource);
            vMachineList.Remove(resource);
            RemoveFromStackProcess(resource);
        }
    }


    void PlayStackSfx()
    {
        AudioSourceManager.Instance.PlayPoPSfx(ascendingCounter);
        ascendingCounter++;
        if (ascendingCounter == 7)
        {
            ascendingCounter = 0;
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

    public override void EnterToResourceDesk(Collider other)
    {
        base.EnterToResourceDesk(other);
        ascendingCounter = 0;
        descendingCounter = 7;


    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        ascendingCounter = 0;
        descendingCounter = 7;


        if (other.gameObject.CompareTag("Trash"))
        {
            trash = null;
        }
    }

    public override void ExitResourceDesk()
    {
        base.ExitResourceDesk();

    }

    public void TapVibrateCustom()
    {
#if UNITY_ANDROID
        Vibration.VibrateAndroid(5);
#endif
    }

}
