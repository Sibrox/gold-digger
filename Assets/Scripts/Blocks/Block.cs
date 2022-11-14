using UnityEngine;
using UnityEngine.Tilemaps;

public enum BlockType
{
    Base = 0,
    Freeze = 1,
    Gta = 2,
    Copper = 3,
    Iron = 4,
    Gold = 5,
    Mimic = 6,
    Unbreakable = 7,
    Caos = 8,
    None = 999
}

public class Block : MonoBehaviour, Breakable
{

    public Player player;
    public Tile tile;
    public Timer timer;

    public double value;
    public BlockType type;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        timer = GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<Timer>();
    }

    public virtual bool OnTap()
    {
        player.AddItemDigged(type.ToString(), value);
        timer.Reset();
        GetComponent<AudioSource>().Play();
        return true;
    }
}
