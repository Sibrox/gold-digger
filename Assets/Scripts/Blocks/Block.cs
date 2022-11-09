using UnityEngine;
using UnityEngine.Tilemaps;
public class Block : MonoBehaviour, Breakable{
    
    public Player player;
    public Tile tile;
    public Timer timer;

    public double value;
    public string name;

    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        timer = GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<Timer>();
    }

    public void onBreak(){

        timer.reset();
    }
}