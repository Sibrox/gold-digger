using UnityEngine;
using UnityEngine.Tilemaps;
public class Block : MonoBehaviour, Breakable{

    public Tile tile;
    public string name;
    public Timer timer;
    public double value;

    public Player player;

    public void onBreak(){

        timer.reset();
    }
}