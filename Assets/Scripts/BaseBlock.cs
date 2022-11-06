using UnityEngine;

public class BaseBlock : MonoBehaviour, Breakable {
    
    public Timer timer;

    void Start() {
        Debug.Log("Spawn");
    }

    void Update() {
        if (Input.GetKey("a"))
        {
            onBreak();
        }
    }

    public void onBreak() {
        timer.reset();
    }
}
