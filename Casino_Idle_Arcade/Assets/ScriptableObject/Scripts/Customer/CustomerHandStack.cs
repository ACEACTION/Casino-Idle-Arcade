using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerHandStack : MonoBehaviour
{
    // variables
    [SerializeField] float stackYOffset;

    // references
    [SerializeField] Transform firstStack;
    [SerializeField] List<CasinoResource> chips = new List<CasinoResource>();



    public void AddChipToStack(CasinoResource chip)
    {
        chips.Add(chip);
        chip.transform.SetParent(transform);
        chip.transform.DOLocalMove(firstStack.localPosition, .7f);
        firstStack.position += new Vector3(0, stackYOffset, 0);
    }

    public void RemoveFromStack()
    {
        
    }

}
