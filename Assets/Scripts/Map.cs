using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public int sizeOfLevel = 10;
    // Start is called before the first frame update
    void Start()
    {   
        var tileMap = this.GetComponent<Tilemap>();
        for (var i = 0; i < sizeOfLevel; i++)
        {
            for (var j = 0; j < sizeOfLevel; j++)
            {   
                
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
