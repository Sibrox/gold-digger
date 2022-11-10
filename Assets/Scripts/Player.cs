using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Dictionary<string,int> playerItems = new Dictionary<string,int>();
    public Map map;
    public int row, col;

    public float x {
        get { return col; }  // get method
    }

    public float y {
        get { return (-row); }  // get method
    }

    public double score;

    bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        col = map.GetSize() / 2 - 1;
        row = 0;
        score = 0;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(x + 0.5f,y + 0.5f,0);
        //this.transform.position = new Vector3(x,y,0);
        if(Input.GetKeyDown("a")) {
            breakLeft();
        }
        if(Input.GetKeyDown("d")) {
            breakRight();
        }
        if(Input.GetKeyDown("s")) {
            Debug.Log("DOwn");
            breakDown();
        } 
        if(Input.GetKeyDown("w")) {
            Debug.Log("DOwn");
            breakUp();
        } 
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
        var broken = map.Break(row,col - 1);
        if(broken) {
            col -=1;
        }
    }

    public void breakRight(){

        var broken = map.Break(row, col + 1);
        if(broken) {
            col +=1;
        }
    }
    public void breakDown(){

        var broken = map.Break(row + 1, col);
        if(broken) {
            row +=1;
        }
    }

    public void breakUp() {
        if(row > 0)
            map.Break(row - 1, col);
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
}
