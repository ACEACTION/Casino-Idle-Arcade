using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deliverecreative2 : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController animatorController;
    [SerializeField] GameObject firstModel;
    [SerializeField] Transform stack;
    [SerializeField] Vector3 offset;

    private void OnEnable()
    {
        var anim = transform.parent.GetComponent<Animator>();
        var hs = transform.parent.GetComponent<HandStack>();
        var chipd = transform.parent.GetComponent<ChipDeliverer>();
        chipd.SetHandStack(stack);
        anim.runtimeAnimatorController = animatorController;
        firstModel.SetActive(false);
    }
}
