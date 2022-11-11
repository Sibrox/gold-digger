using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int sizeOfLevel = 20;

    public Block[,] blocks;
    public Block none;

    public string[] blocksPath;

    Tilemap tileMap;
    Tilemap background;
    
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
        none = Instantiate(Resources.Load ("Prefab/Blocks/" + BlockType.None) as GameObject).GetComponent<Block>();
        for (var row = 1; row < sizeOfLevel - 1; row++)
        {
            for (var col = 1; col < sizeOfLevel - 1; col++)
            {   
                int index = Random.Range(0,8);
                Debug.Log("Prefab/Blocks/" + blocksPath[index]);

                blocks[row,col] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[index]) as GameObject).GetComponent<Block>();
                tileMap.SetTile(new Vector3Int(col,-row,0), blocks[row,col].tile);
            }
        }

        for (var row = 0; row < sizeOfLevel; row++) {

            blocks[row, 0] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            blocks[row, sizeOfLevel - 1] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            tileMap.SetTile(new Vector3Int(0, -row, 0), blocks[row,0].tile);
            tileMap.SetTile(new Vector3Int(sizeOfLevel - 1, -row, 0), blocks[row,sizeOfLevel - 1].tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSize() {
        return sizeOfLevel;
    }

    public bool Break(int row, int col)
    {
        var broken = blocks[row,col].onTap();
        if(broken) {
            blocks[row,col] = none;
            tileMap.SetTile(new Vector3Int(col,-row,0),none.tile);
        }

        return broken;
    }
}
