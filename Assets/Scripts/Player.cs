using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Dictionary<string,int> playerItems = new Dictionary<string,int>();
    public Map map;
    public int x,y;
    public double score;
    
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        score = 0;
        y = map.sizeOfLevel/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a")) {
            Debug.Log("LEFT");
            breakLeft();
        }
        if(Input.GetKey("d")) {
            Debug.Log("RIGHT");
            breakRight();
        }
    }

    string getTotalBlockDigged(){

        int totalBlockDigged = 0;
        string recapLevelString = new string("");

        foreach (var item in playerItems)
        {
            recapLevelString += item.Key+" :"+item.Value+"\n";
            totalBlockDigged += item.Value;
        }

        return recapLevelString.Insert(0,"Total block digged: "+totalBlockDigged);
    }
    public void breakLeft(){

        map.Break(--x,y);
    }

    public void breakRight(){

        map.Break(++x,y);
    }
    public void breakDown(){

        map.Break(x,++y);
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
