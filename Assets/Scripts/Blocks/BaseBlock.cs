using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseBlock : Block {
    

    void Update() {
        player.addItemDigged(name,value);
    }
}   
