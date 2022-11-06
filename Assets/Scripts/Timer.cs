using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public double timer;

    public double baseTime = 5.0;
    // Start is called before the first frame update
    void Start()
    {
        timer = baseTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0 ) {
            Debug.Log("GAME OVER");
        }
        timer -= Time.deltaTime;
    }

    public void reset() {
        timer = baseTime;
    }
}
