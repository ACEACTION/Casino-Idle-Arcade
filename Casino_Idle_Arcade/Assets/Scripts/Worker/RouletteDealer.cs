using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteDealer : Worker
{
    public override void ActiveActionAnim(bool state)
    {
        base.ActiveActionAnim(state);
        anim.SetBool("isPlayingCard", state);
    }


}
