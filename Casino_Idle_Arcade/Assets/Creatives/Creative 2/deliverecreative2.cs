using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class deliverecreative2 : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController animatorController;
    [SerializeField] GameObject firstModel;
    [SerializeField] ChipDeliverer cd;
    [SerializeField] Transform stack;
    [SerializeField] Vector3 offset;
    [SerializeField] WorkerHandStack handstack;
    [SerializeField] Transform handstackFirst;

    private void OnEnable()
    {
        var anim = transform.parent.GetComponent<Animator>();
        cd.SetHandStack(handstack);
        anim.runtimeAnimatorController = animatorController;
        firstModel.SetActive(false);
        handstackFirst.gameObject.SetActive(false);
    }
}
