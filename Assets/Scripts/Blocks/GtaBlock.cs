using UnityEngine;

public class GtaBlock : MonoBehaviour, Breakable {
    
    public Timer timer;
    public int resistence;
    private KeyCode[] pool;
    public KeyCode[] sequence;
    public int index = 0;
    public double score;

    void Start() {
        Debug.Log("Spawn");
        resistence = 5;
        pool = new KeyCode[] {KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow};
        sequence = new KeyCode[resistence];
        for(var i=0; i<resistence; i++){
            sequence[i] = pool[Random.Range(0,3)];
        }
    }

    void Update() {
        if(Input.GetKey(sequence[index])){
            resistence--;
            index++;
        }
        else if(!Input.GetKey(sequence[index])){
            //onStun();
        }
        if(resistence <= 0)
        {
            onBreak();
        }
    }

    public void onBreak() {
        score = timer.timer;
        timer.reset();
        //destroy
    }
}