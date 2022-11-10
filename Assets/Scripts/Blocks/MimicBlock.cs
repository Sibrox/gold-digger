using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicBlock : Block
{

    public override bool onTap(){
        player.addItemDigged(type.ToString(),value);
        timer.gameOver();
        return true;
    }
}
