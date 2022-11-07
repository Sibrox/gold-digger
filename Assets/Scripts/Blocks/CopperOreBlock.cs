public class CopperOreBlock : MonoBehaviour, BaseBlock {
    
    public Timer timer;

    void Start() {
        Debug.Log("Spawn");
        public int resistence = 2;
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
