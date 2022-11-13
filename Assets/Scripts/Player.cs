using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Dictionary<string,int> playerItems = new Dictionary<string,int>();
    public Map map;
    public int row, col; 
    public double score,stunDuration, caosDuration;
    public bool isMoving, stopped, stunned, confused;

    public float x {
        get { return col; }  // get method
    }

    public float y {
        get { return (-row); }  // get method
    }

    void Start()
    {
        col = map.GetSize() / 2 - 1;
        row = 0;
        score = stunDuration = caosDuration = 0;
        stopped = isMoving = stunned = confused = false;
    }
    void Update()
    {
        if(!stopped){

            this.transform.position = new Vector3(x + 0.5f,y + 0.5f,-10);

            if(Input.GetKeyDown("a")) {

                breakLeft();
            }
            if(Input.GetKeyDown("d")) {

                breakRight();
            }
            if(Input.GetKeyDown("s")) {
                
                breakDown();
            } 
            if(Input.GetKeyDown("w")) {
                
                breakUp();
            } 
        }
        else{

            checkStunnedState();
        }

        checkConfusedState();
    }

    public string getTotalBlockDigged(){

        int totalBlockDigged = 0;
        string recapLevelString = new string("");

        foreach (var item in playerItems)
        {
            recapLevelString += item.Key+" :"+item.Value + "; ";
            totalBlockDigged += item.Value;
        }
        
        return recapLevelString.Insert(0,"Total block digged: "+totalBlockDigged);
    }

    public void breakLeft(){

        if(!confused){

            var broken = map.Break(row,col - 1);

            if(broken) {
                isMoving = true;
                col -=1;
            }
        }
        else{

            var broken = map.Break(row,col + 1);

            if(broken) {
                isMoving = true;
                col +=1;
            }
        }
    }

    public void breakRight(){

        if(!confused){

            var broken = map.Break(row, col + 1);
            if(broken) {
                isMoving = true;
                col +=1;
            }
        }
        else{

            var broken = map.Break(row,col - 1);

            if(broken) {
                isMoving = true;
                col -=1;
            }
        }   
    }
    public void breakDown(){

        if(!confused){

            var broken = map.Break(row + 1, col);
            if(broken) {
                isMoving = true;
                row +=1;
            }
        }
        else{

            if(row > 0)
            map.Break(row - 1, col);
        }
        
    }

    public void breakUp() {

        if(!confused){

            if(row > 0)
            map.Break(row - 1, col);
        }
        else{

            var broken = map.Break(row + 1, col);
            if(broken) {
                isMoving = true;
                row +=1;
            }
        }
    }

   public void addItemDigged(string item, double value){

        if(playerItems.ContainsKey(item)){

            playerItems[item]++;
        }
        else{
            playerItems.Add(item,1);
        }

        score += value;
    }

    public void setStopped(bool stopped){

        this.stopped = stopped;
    }

    public void stun(double stunDuration){

        stopped = true;
        stunned = true;
        this.stunDuration = stunDuration;
    }

    public void confuse(double caosDuration){

        confused = true;
        this.caosDuration = caosDuration;
    }

    private void checkConfusedState(){

        if(caosDuration < 0){

            confused = false;
        }
        else if(caosDuration > 0){

            caosDuration -= Time.deltaTime;
        }
    }

    private void checkStunnedState(){

    if(stunned){

        if(stunDuration < 0){

            stopped = stunned = false;
        }

    stunDuration -= Time.deltaTime;
    }
    }
}
