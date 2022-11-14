using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBlock : Block
{
    public double freezingTimer;

    public override bool OnTap()
    {
        player.AddItemDigged(type.ToString(), value);
        timer.Freeze(freezingTimer);
        return true;
    }
}
