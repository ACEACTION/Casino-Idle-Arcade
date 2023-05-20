using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stack/Player Hand Stack")]
public class PlayerHandStackData : HandStackData
{
    public override void PassValueToData(int v)
    {
        base.PassValueToData(v);

        // for player stack max txt in ui
        MaxStackText.Instance.SetTextState(false);
    }
}
