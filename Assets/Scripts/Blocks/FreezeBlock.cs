using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBlock : Block
{
    public double freezingTimer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            onTap();
        }
    }

    new void onTap(){

        timer.freeze(freezingTimer);
    }
}
