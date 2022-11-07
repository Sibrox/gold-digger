using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        var tileMap = this.GetComponent<Tilemap>();
        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {   
                var block = new BaseBlock();
                tileMap.SetTile(new Vector3Int(i,j,0),block.tile);
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
