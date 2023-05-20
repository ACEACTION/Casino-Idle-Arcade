using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaccaratDealer : Employee
{
    public override void ActiveActionAnim(bool state)
    {
        anim.SetBool("isPlayingCard", state);
        base.ActiveActionAnim(state);
    }

}
