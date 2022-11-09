using UnityEngine;

public class GtaBlock : Block {
    public int resistence = 5;
    private KeyCode[] pool,sequence;
    public int index = 0;
    public double score;
    public bool started = false;

    void Start() {
        Debug.Log("Spawn");
        pool = new KeyCode[] {KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};
        sequence = new KeyCode[resistence];
        for(var i=0; i<resistence; i++){
            sequence[i] = pool[Random.Range(0,3)];
        }
    }

    void Update() {

    if(started){

        score -= Time.deltaTime;

        if(Input.GetKey(sequence[index])){
            resistence--;
            index++;
        }
        else if(!Input.GetKey(sequence[index])){
            started = false;
            //onStun();
        }
        if(resistence == 0)
        {
            started = false;
            //reward
            //destroy
        }
    }
        
    }

    public void onBreak() {
        
        timer.reset();
        score = timer.timer;
        started = true;
    }
}