using UnityEngine;

public class IronOreBlock : MonoBehaviour, Breakable {
    
    public Timer timer;
    public Player player;
    public int resistence = 3;

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