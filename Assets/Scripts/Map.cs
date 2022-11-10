using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int sizeOfLevel = 20;

    public Block[,] blocks;
    public Block broken;

    public string[] blocksPath;

    Tilemap tileMap;
    void Awake() {
        tileMap = this.GetComponent<Tilemap>();
        blocksPath = new string[8];
        for(var i = 0; i <  blocksPath.Length; i++) {
            blocksPath[i] = ((BlockType) i).ToString();
        }

        blocks = new Block[sizeOfLevel, sizeOfLevel];
    }

    // Start is called before the first frame update
    void Start()
    {   
        broken = Instantiate(Resources.Load ("Prefab/Blocks/" + BlockType.None) as GameObject).GetComponent<Block>();
        for (var row = 0; row < sizeOfLevel; row++)
        {
            for (var col = 0; col < sizeOfLevel; col++)
            {   
                int index = Random.Range(0,8);
                Debug.Log("Prefab/Blocks/" + blocksPath[index]);

                blocks[row,col] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[0]) as GameObject).GetComponent<Block>();
                
                Debug.Log("Loaded");
                var currentTile = blocks[row,col].tile;
                tileMap.SetTile(new Vector3Int(col,-row,0), currentTile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSize() {
        return sizeOfLevel;
    }

    public void Break(int row, int col)
    {
        blocks[row,col].onBreak();
        tileMap.SetTile(new Vector3Int(col,-row,0),broken.tile);
    }
}
