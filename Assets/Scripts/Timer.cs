using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public double timer,freezingTimer;
    public double baseTime = 5.0;
    public bool isOver = false;
    public bool isFreezed = false;

    public Player player;
    void Start()
    {
        timer = baseTime;
    }

    void Update()
    {
        if(timer < 0) {
            this.gameOver();
            Debug.Log("GAME OVER");
        }
        if(!isFreezed && !isOver){

            timer -= Time.deltaTime;
        }
        
        if(isFreezed){   

            freezingTimer -= Time.deltaTime;

            if(freezingTimer < 0){

                freezingTimer = 0;
                isFreezed = false;
            }
        }   
    }

    public void reset() {

        timer = baseTime;
    }

    public void gameOver(){

        timer = 0;
        isOver = true;
        var result = player.getTotalBlockDigged();
        Debug.Log(result);
        Debug.Log(player.score);
    }

    public void freeze(double freezingTimer){

        this.freezingTimer = freezingTimer;
        isFreezed = true;
    }
}
