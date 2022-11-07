using UnityEngine;

public class GoldOreBlock : MonoBehaviour, Breakable {
    
    public Timer timer;
    public Player player;
    public int resistence = 4;

    void Start() {
        Debug.Log("Spawn");
    }

    void Update() {
        if(Input.GetKey("a")){
            resistence--;
        }
        if(resistence <= 0)
        {
            onBreak();
        }
    }

    public void onBreak() {
        timer.reset();
    }
}