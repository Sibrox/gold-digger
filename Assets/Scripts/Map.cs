using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction {
    DOWN = 0,
    LEFT = 1,
    RIGHT = 2
}

public class Map : MonoBehaviour
{

    public int CHUNK_WIDTH = 10;
    public int CHUNK_HEIGHT = 10;
    
    public int sizeOfLevel = 20;

    public Block[,] blocks;
    public Block none;

    public string[] blocksPath;

    Tilemap tileMap;
    Tilemap background;
    
    void Awake() {

        CHUNK_WIDTH = 10;
        CHUNK_HEIGHT = 10;
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

                blocks[row,col] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[0]) as GameObject).GetComponent<Block>();
                tileMap.SetTile(new Vector3Int(col,-row,0), blocks[row,col].tile);
            }
        }

        for (var row = 0; row < sizeOfLevel; row++) {

            blocks[row, 0] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            blocks[row, sizeOfLevel - 1] = Instantiate(Resources.Load ("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            tileMap.SetTile(new Vector3Int(0, -row, 0), blocks[row,0].tile);
            tileMap.SetTile(new Vector3Int(sizeOfLevel - 1, -row, 0), blocks[row,sizeOfLevel - 1].tile);
        }

        generateChunck(new int[]{1, 4, 8});
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


    public void generateChunck(int[] entryPoints ) {

        int[][] chunk = new int[CHUNK_HEIGHT][];

        for (var i = 0; i < CHUNK_HEIGHT; i++ ) {
            chunk[i] = new int[CHUNK_WIDTH];
        }

        for (var i = 0; i < 3; i++) {
            chunk[0][entryPoints[i]] = 1;
            findPath(chunk, entryPoints[i]);
        }

        string matrix = "";

        for (int i = 0; i < CHUNK_HEIGHT; i++) {
            for (int j = 0; j < CHUNK_WIDTH; j++) {
                matrix += (chunk[i][j] + "\t");
            } 
            matrix += "\n";
        } 
        Debug.Log(matrix);
    }

    void findPath(int[][] chunk, int xStart) {

        int row = 0;
        int x = xStart;
        Direction lastDirection = Direction.DOWN;

        while (row != CHUNK_HEIGHT - 1) {

            int percentage = Random.Range(0,100);
            Direction currentDirection = Direction.DOWN;

            if(x == 0 || x == CHUNK_WIDTH - 1){
                currentDirection = Direction.DOWN;
            }
            else {
            
                if (percentage < 60) {
                    currentDirection = Direction.DOWN;
                }
                else if(percentage >= 60) {
                    if( lastDirection == Direction.LEFT) {
                        currentDirection = Direction.LEFT;
                    }
                    else if( lastDirection == Direction.RIGHT) {
                        currentDirection = Direction.RIGHT;
                    }
                    else {
                        if(percentage >= 60 && percentage < 80) {
                            currentDirection = Direction.LEFT;
                        }
                        else {
                            currentDirection = Direction.RIGHT;
                        }
                    }
                }
            }   

            lastDirection = currentDirection;
            switch (currentDirection) {
                case Direction.DOWN: 
                    chunk[++row][x] = 1;
                    break;
                case Direction.LEFT:
                    chunk[row][--x] = 1;
                    break;
                case Direction.RIGHT:
                    chunk[row][++x] = 1;
                    break;
            }
        }

    }
}
