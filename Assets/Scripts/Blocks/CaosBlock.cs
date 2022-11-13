using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaosBlock : Block{

    public double caosDuration;

    public override bool onTap(){

        player.confuse(caosDuration);
        return true;
    }
}