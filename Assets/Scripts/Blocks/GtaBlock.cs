using UnityEngine;

public class GtaBlock : Block {
    public int resistence = 5;
    private KeyCode[] pool,sequence;
    public int index = 0;
    public double score;
    public bool started;
    public int counter = 0;
    public double slowRate = 2.0;
    public override void Start() {

        base.Start();
        started = false;
        pool = new KeyCode[] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
        sequence = new KeyCode[resistence];

        for(var i=0; i<resistence; i++){
            sequence[i] = pool[Random.Range(0,3)];
        }
    }

    void Update() {

        if(started){

            score -= Time.deltaTime/slowRate;

            if(Input.anyKeyDown){ counter++;} //prende l'input dell'onTap()

            if(Input.anyKeyDown && counter >= 2){

                if(Input.GetKeyDown(sequence[index])){
            
                    resistence--;
                    index++;
                }
                else{
                    started = false;
                    Debug.Log("SONO STUNNATO!!");
                    player.stun(2.0);
                }
                if(resistence == 0){
                    started = false;
                    player.setStopped(false);
                    Debug.Log("L'ho smurfata ho fatto "+score+" punti");
                    timer.slowed = false;
                    timer.reset();
                    //reward
                    //destroy
                }
            }
        }   
    }

    public override bool onTap() {
        
        player.setStopped(true);
        timer.slowTime(slowRate);
        timer.reset();
        Debug.Log(debug());
        score = timer.timer;
        started = true;
        player.addItemDigged(type.ToString(),value);
        return true;
    }

    private string debug(){

        string debugString = new string("");

        for(int i = 0; i < resistence;i++){

            debugString += sequence[i].ToString() + " ";
        }

        return debugString;
    }
}