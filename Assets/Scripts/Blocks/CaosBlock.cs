using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaosBlock : Block
{

    public double caosDuration;

    public override bool OnTap()
    {
        player.Confuse(caosDuration);
        return true;
    }
}
