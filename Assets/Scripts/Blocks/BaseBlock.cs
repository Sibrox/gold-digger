using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseBlock : MonoBehaviour, Breakable {
    
    //public Tile tile;
    public Timer timer;
    public Tile tile;

    public BaseBlock() {
        tile = new Tile();
        Texture2D texture = Resources.Load<Texture2D>("Tiles/block") as Texture2D;
        tile.sprite = Sprite.Create(texture,
            new Rect(0, 0,512, 512), // section of texture to use
            new Vector2(0.5f, 0.5f), // pivot in centre
            512, // pixels per unity tile grid unit
            1,
            SpriteMeshType.Tight,
            Vector4.zero);
    }

    void Start() {
        
    }

    void Update() {
        Debug.Log("PROVA");
    }

    public void onBreak() {
        timer.reset();
    }
}
