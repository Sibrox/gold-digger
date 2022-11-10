using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBlock : Block
{
    public double freezingTimer;

    public override bool onTap(){

        timer.freeze(freezingTimer);
        return true;
    }
}
