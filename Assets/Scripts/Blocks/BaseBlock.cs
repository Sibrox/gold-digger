using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseBlock : Block {
    
    void Start() {
        
    }

    void Update() {
        player.addItemDigged(name,value);
    }
}   
