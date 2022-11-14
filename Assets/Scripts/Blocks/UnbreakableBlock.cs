using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakableBlock : Block
{
    public double stunTimer;

    public override bool OnTap()
    {
        player.Stun(1.0);
        GetComponent<AudioSource>().Play();
        return false;
    }
}
