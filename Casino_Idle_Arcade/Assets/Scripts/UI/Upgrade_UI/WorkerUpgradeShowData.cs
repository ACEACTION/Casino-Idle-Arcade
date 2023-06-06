using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/UI/WorkerUpgradeShowData")]
public class WorkerUpgradeShowData : ScriptableObject
{
    [HideInInspector] public Vector3 upgradeMsgPanelOriginPos;
    public float yMoveTarget;
    public float yMoveDuration;
}
