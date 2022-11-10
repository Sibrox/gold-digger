using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseBlock : Block {
    

    void Update() {
        player.addItemDigged(type.ToString(),value);
    }
}   
