using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBlock : Block{

    public int resistence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a")){
            resistence--;
        }
        if(resistence <= 0)
        {
            onBreak();
        }
    }


      new void onBreak() {
        
        player.addItemDigged(name,value);
        base.onBreak();
       
    }
}

