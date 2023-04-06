using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        transform.DOScale(5, 3).SetLoops(-1, LoopType.Yoyo);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            transform.DOKill();
            //DOTween.Kill(transform);
    }
}
