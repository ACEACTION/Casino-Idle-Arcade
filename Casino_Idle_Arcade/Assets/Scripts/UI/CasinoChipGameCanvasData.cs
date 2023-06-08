using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UI/Casino Chip Game Canvas Data")]
public class CasinoChipGameCanvasData : ScriptableObject
{
    public float duration;
    public float yMoveTarget;
    public Ease openPanelEaseType;
    public Ease closePanelEaseType;
    [HideInInspector] public Vector3 originPos;

}
