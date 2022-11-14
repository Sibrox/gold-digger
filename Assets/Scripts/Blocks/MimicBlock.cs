using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicBlock : Block
{

    public override bool OnTap()
    {
        player.AddItemDigged(type.ToString(), value);
        timer.GameOver();
        return true;
    }
}
