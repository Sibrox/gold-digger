using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakableBlock : Block
{
    public double stunTimer;

    // Update is called once per frame
    void Update()
    {

    }

    public override bool onTap(){
        player.stun(1.0);
        return false;
    }
}
