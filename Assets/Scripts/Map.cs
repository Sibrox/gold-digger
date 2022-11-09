using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int sizeOfLevel = 10;

    public GameObject[,] blocks;

    // Start is called before the first frame update
    void Start()
    {   
        blocks = new GameObject[sizeOfLevel, sizeOfLevel];

        var tileMap = this.GetComponent<Tilemap>();
        for (var i = 0; i < sizeOfLevel; i++)
        {
            for (var j = 0; j < sizeOfLevel; j++)
            {   
                blocks[i,j] = Instantiate(Resources.Load ("Prefab/Blocks/Base") as GameObject);
                var currentTile = blocks[i,j].GetComponent<BaseBlock>().tile;
                tileMap.SetTile(new Vector3Int(i,j,0), currentTile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break(int axis_x, int axis_y)
    {
        
    }
}
