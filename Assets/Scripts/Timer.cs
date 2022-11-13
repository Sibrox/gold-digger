using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public double timer,freezingTimer,slowRate;
    public double baseTime = 5.0;
    public bool over = false;
    public bool freezed = false;
    public bool slowed = false;
    public Player player;
    void Start()
    {
       
        timer = baseTime;
        slowRate = 0;
    }

    void Update(){

        if(!slowed){

            if(timer < 0) {
                this.gameOver();
                Debug.Log("GAME OVER");
            }
            if(!freezed && !over){

                timer -= Time.deltaTime;
            }
            if(freezed){   
                freezingTimer -= Time.deltaTime;
                if(freezingTimer < 0){

                    freezingTimer = 0;
                    freezed = false;
                }

                // BUG INTERAZIONE FREZZE + SLOW TIME CON GTA BLOCK-
            }
        }
        else{

            if(timer < 0) {
                this.gameOver();
                Debug.Log("GAME OVER");
            }
            
            timer -= Time.deltaTime/slowRate;
        } 

        GetComponent<Image>().fillAmount = (float)(timer/baseTime);
    }

    public void reset() {

        timer = baseTime;
    }

    public void gameOver(){

        timer = 0;
        over = true;
        var result = player.getTotalBlockDigged();
        Debug.Log(result);
        Debug.Log(player.score);
    }

    public void freeze(double freezingTimer){

        this.freezingTimer = freezingTimer;
        freezed = true;
    }

    public void slowTime(double slowRate){

        this.slowRate = slowRate;
        this.slowed = true;
    }
}
