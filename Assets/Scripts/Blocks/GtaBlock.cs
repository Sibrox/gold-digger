using UnityEngine;

public class GtaBlock : Block {
    public int resistence = 5;
    private KeyCode[] pool,sequence;
    public int index = 0;
    public double score;
    public bool started;
    public int counter = 0;
    public override void Start() {

        started = false;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        timer = GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<Timer>();
        pool = new KeyCode[] {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
        sequence = new KeyCode[resistence];

        for(var i=0; i<resistence; i++){
            sequence[i] = pool[Random.Range(0,3)];
        }
    }

    void Update() {

        if(started){

            score -= Time.deltaTime;

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
                    //reward
                    //destroy
                }
            }
        }
    }

    public override bool onTap() {
        
        player.setStopped(true);
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