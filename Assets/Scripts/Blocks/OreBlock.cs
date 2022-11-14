using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBlock : Block
{

    public int resistence;

    public override bool OnTap()
    {
        resistence -= 1;
        if (resistence > 0) return false;

        timer.Reset();
        player.AddItemDigged(type.ToString(), value);
        return true;
    }
}
