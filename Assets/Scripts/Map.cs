using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    DOWN = 0,
    LEFT = 1,
    RIGHT = 2
}
public struct SubChunkInfo
{
    public int rotation;
    public int counter;
    public bool opposite;
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
    public Player player;

    void Awake()
    {
        CHUNK_WIDTH = 10;
        CHUNK_HEIGHT = 10;
        tileMap = this.GetComponent<Tilemap>();
        blocksPath = new string[9];
        for (var i = 0; i < blocksPath.Length; i++)
        {
            blocksPath[i] = ((BlockType)i).ToString();
        }

        blocks = new Block[sizeOfLevel, sizeOfLevel];
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Animated Tile
    }

    public int GetSize()
    {
        return sizeOfLevel;
    }

    public bool Break(int row, int col)
    {
        var broken = blocks[row, col].OnTap();
        if (broken)
        {
            blocks[row, col] = none;
            tileMap.SetTile(new Vector3Int(col, -row, 0), none.tile);
        }

        return broken;
    }


    public int[][] GenerateChunck(int[] entryPoints)
    {

        int[][] chunk = new int[CHUNK_HEIGHT][];

        for (var i = 0; i < CHUNK_HEIGHT; i++)
        {
            chunk[i] = new int[CHUNK_WIDTH];
        }

        for (var i = 0; i < 3; i++)
        {
            chunk[0][entryPoints[i]] = i + 1;
            FindPath(chunk, entryPoints[i], i + 1);
        }

        string matrix = "";

        for (int i = 0; i < CHUNK_HEIGHT; i++)
        {
            for (int j = 0; j < CHUNK_WIDTH; j++)
            {
                matrix += (chunk[i][j] + "\t");
            }
            matrix += "\n";
        }
        Debug.Log(matrix);

        return chunk;
    }

    void FindPath(int[][] chunk, int xStart, int nPath)
    {
        int row = 0;
        int col = xStart;
        Direction lastDirection = Direction.DOWN;

        var downRate = 60.0;
        var sideRate = (100.0 - downRate) / 2;

        while (row != CHUNK_HEIGHT - 1)
        {
            int percentage = Random.Range(0, 100);
            Direction currentDirection = Direction.DOWN;

            if (percentage < downRate)
            {
                currentDirection = Direction.DOWN;
            }
            else if (percentage >= downRate && percentage < 100)
            {
                if (col == 0)
                {
                    currentDirection = lastDirection == Direction.DOWN ? Direction.RIGHT : Direction.DOWN;
                }
                else if (col == CHUNK_WIDTH - 1)
                {
                    currentDirection = lastDirection == Direction.DOWN ? Direction.LEFT : Direction.DOWN;
                }

                else if ((100 - percentage) < sideRate)
                {
                    currentDirection = lastDirection == Direction.DOWN ? Direction.RIGHT : lastDirection;
                }
                else
                {
                    currentDirection = lastDirection == Direction.DOWN ? Direction.LEFT : lastDirection;
                }
            }

            switch (currentDirection)
            {
                case Direction.DOWN:
                    chunk[++row][col] = nPath;
                    break;
                case Direction.LEFT:
                    chunk[row][--col] = nPath;
                    break;
                case Direction.RIGHT:
                    chunk[row][++col] = nPath;
                    break;
            }

            lastDirection = currentDirection;
        }
    }

    void FillMap(int[][] chunk)
    {
        for (int i = 0; i < CHUNK_WIDTH; i++)
        {
            for (int j = 0; j < CHUNK_HEIGHT; j++)
            {
                if (chunk[i][j] > 0)
                {
                    SubChunkInfo info = GetSubChunkInfo(chunk, i, j, chunk);
                    //BlockType[][] subChunk = GenerateSubChunk(info);
                }
            }
        }
    }
    /*
    private BlockType[][] GenerateSubChunk(SubChunkInfo info)
    {

    }
    */

    private SubChunkInfo GetSubChunkInfo(int[][] chunk, int i, int j, int[][] prevChunk)
    {
        SubChunkInfo subChunkInfo;
        subChunkInfo.counter = 0;
        subChunkInfo.rotation = 0;
        subChunkInfo.opposite = false;

        if ((i == 0 && prevChunk[CHUNK_HEIGHT - 1][j] > 0) || (i >= 0 && chunk[i - 1][j] > 0))
        {
            subChunkInfo.counter++;
        }

        if (j + 1 < CHUNK_WIDTH && chunk[i][j + 1] > 0 && subChunkInfo.counter++ == 0)
        {
            subChunkInfo.rotation = 1;

        }
        else if (subChunkInfo.counter == 1)
        {
            subChunkInfo.opposite = true;
        }

        if (i == CHUNK_HEIGHT - 1 || (i + 1 < CHUNK_HEIGHT && chunk[i + 1][j] > 0))
        {
            if (subChunkInfo.counter++ == 0)
            {

                subChunkInfo.rotation = 2;
            }
        }
        else if (subChunkInfo.counter == 1)
        {
            subChunkInfo.opposite = true;
        }

        if (j - 1 >= 0 && chunk[i][j - 1] > 0 && subChunkInfo.counter++ == 0)
        {
            subChunkInfo.rotation = 3;

        }
        else if (subChunkInfo.counter == 1)
        {

            subChunkInfo.opposite = true;
        }

        return subChunkInfo;
    }
    private void GenerateWorld()
    {
        none = Instantiate(Resources.Load("Prefab/Blocks/" + BlockType.None) as GameObject).GetComponent<Block>();
        for (var row = 1; row < sizeOfLevel - 1; row++)
        {
            for (var col = 1; col < sizeOfLevel - 1; col++)
            {
                int index = Random.Range(0, 9);
                Debug.Log("Prefab/Blocks/" + blocksPath[index]);

                blocks[row, col] = Instantiate(Resources.Load("Prefab/Blocks/" + blocksPath[index]) as GameObject).GetComponent<Block>();
                tileMap.SetTile(new Vector3Int(col, -row, 0), blocks[row, col].tile);
            }
        }

        for (var row = 0; row < sizeOfLevel; row++)
        {

            blocks[row, 0] = Instantiate(Resources.Load("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            blocks[row, sizeOfLevel - 1] = Instantiate(Resources.Load("Prefab/Blocks/" + blocksPath[((int)BlockType.Unbreakable)]) as GameObject).GetComponent<Block>();
            tileMap.SetTile(new Vector3Int(0, -row, 0), blocks[row, 0].tile);
            tileMap.SetTile(new Vector3Int(sizeOfLevel - 1, -row, 0), blocks[row, sizeOfLevel - 1].tile);
        }

        var chunk = GenerateChunck(new int[] { 1, 4, 8 });
        //wFillMap(chunk);
    }

    public void Restart()
    {
        player.Init();
        GenerateWorld();
    }
}
