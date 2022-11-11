using UnityEngine;

public class GtaBlock : Block {
    public int resistence = 5;
    public KeyCode[] pool,sequence;
    public int index = 0;
    public double maxScore = 10000;
    public double slowRate = 2.0;
    public bool started = false;
    public double stunTime = 2;

    void Start() {
        Debug.Log("Spawn");
        pool = new KeyCode[] {KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};
        sequence = new KeyCode[resistence];
        for(var i=0; i<resistence; i++){
            sequence[i] = pool[Random.Range(0,3)];
        }
    }

    void Update() {

        if(started) {
            if(Input.GetKey(sequence[index])){
                resistence--;
                index++;
            }
            else if(!Input.GetKey(sequence[index])){
                started = false;
                player.stun(stunTime);
            }
            if(resistence == 0)
            {
                started = false;
                //reward
                //destroy
            }
        }
    }

    public override bool onTap() {
        
        timer.reset();
        timer.slow(slowRate);
        started = true;
        player.stop();
        return true;
    }
}