using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBlock : MonoBehaviour, Breakable
{
    public Timer timer; 
    public double freezingTimer;

    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            onBreak();
        }
    }

    public void onBreak(){

        timer.freeze(freezingTimer);
    }
}
