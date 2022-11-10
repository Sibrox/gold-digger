using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBlock : Block{

    public int resistence;

      public override bool onTap() {
        resistence -= 1;
        if( resistence <= 0) {
            player.addItemDigged(type.ToString(),value);
            return true;
        }
        else {
            return false;
        }
    }
}

