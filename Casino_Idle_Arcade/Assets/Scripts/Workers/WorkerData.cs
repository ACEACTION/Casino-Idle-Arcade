using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/WorkerData")]
public class WorkerData : UpgradeData<float>
{
    public float moveSpeed;
    public float defaultMoveSpeed;

    public override void PassValueToData(float v)
    {
        base.PassValueToData(v);
        moveSpeed = v;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        //moveSpeed = defaultMoveSpeed;
    }

    public override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        upgradeValue = 0;
        moveSpeed = defaultMoveSpeed;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        LoadDefaultValue();
    }

    public override void ResetData()
    {
        LoadDefaultValue();
        base.ResetData();
    }
}
