using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicBlock : Block
{

    public override bool onTap(){
        timer.gameOver();
        return true;
    }
}
