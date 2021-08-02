using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] Transform Ground = default;
    [SerializeField] BoardTile tilePrefab = default;
    
    Vector2Int BoardSize;
    BoardTile[,] tiles;

    private void Update()
    {
        ClickOnTile();
    }

    public void InitializeGameBoard(Vector2Int Size)
    {
        BoardSize = Size;
        Ground.localScale = new Vector3(Size.x, Size.y, 1);
        //instantiate board tiles:
        tiles = new BoardTile[Size.y, Size.x];
        

        float pos_y = (-Size.y / 2f) + 0.5f;
        for (int row = 0; row<Size.y; row++)
        {
            float pos_x = (-Size.x / 2f) + 0.5f;
            for (int col = 0; col < Size.x; col++)
            {
                
                BoardTile tile = tiles[row,col] = Instantiate(tilePrefab);
                tile.transform.SetParent(this.transform);
                tile.transform.position = new Vector3(pos_x,  1f, pos_y);

                pos_x++;
            }
            pos_y++;
        }
        GameTilesSetNeighbours(Size);
    }

    void GameTilesSetNeighbours(Vector2Int Size)
    {
   
        for (int row = 0; row < Size.y; row++)
        {
            for(int col =0; col < Size.x; col++)
            {
                BoardTile n = null, s = null, e = null, w = null;
                if (col - 1 >= 0) w = tiles[row, col-1];
                if (col + 1 < Size.x) e = tiles[row, col+1];
                if (row - 1 >= 0) s = tiles[row-1 , col];
                if (row + 1 < Size.y) n = tiles[row+1 , col];

                tiles[row,col].SetNeighbours(n, s, e, w);
            }
        }
    }

    void SetNewDestination(BoardTile board)
    {
         
        if (board)
        {
            ClearPath(BoardSize);
            board.SetDestination();
            //use board as starting point and perform bfs.
            
            BFS(board);
            
        }
    }

    void ClearPath(Vector2Int Size)
    {
        //clear path for all tiles
        for (int row = 0; row < Size.y; row++)
        {
            for (int col = 0; col < Size.x; col++)
            {
                tiles[row, col].ClearPath();
            }
        }
    }

    void BFS(BoardTile destTile)
    {
        Queue<BoardTile> tileq = new Queue<BoardTile>();
        tileq.Enqueue(destTile);
        while (tileq.Count > 0)
        {
            BoardTile t = tileq.Dequeue();
            
            int d = t.GetNewDistanceToDest();
            if (t.North!=null && !t.North.hasPath)
            {
                tileq.Enqueue(t.North);
                t.North.SetNextTileOnPath(t);
                t.North.SetNewDistanceToDest(d + 1);
                //point down
                t.North.ShowPathPointer(2);
            }
            if (t.South != null && !t.South.hasPath)
            {
                tileq.Enqueue(t.South);
                t.South.SetNextTileOnPath(t);
                t.South.SetNewDistanceToDest(d + 1);
                //point up
                t.South.ShowPathPointer(1);
            }
            if (t.East != null && !t.East.hasPath)
            {
                tileq.Enqueue(t.East);
                t.East.SetNextTileOnPath(t);
                t.East.SetNewDistanceToDest(d + 1);
                //point left
                t.East.ShowPathPointer(3);
            }
            if (t.West != null && !t.West.hasPath)
            {
                tileq.Enqueue(t.West);
                t.West.SetNextTileOnPath(t);
                t.West.SetNewDistanceToDest(d + 1);
                //point right
                t.West.ShowPathPointer(4);
            }

        }
    }

    void ClickOnTile()
    {
        //click to set dest
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int x = (int)(hit.point.x + BoardSize.x * 0.5f);
                int y = (int)(hit.point.z + BoardSize.y * 0.5f);
                if ((x >= 0 && x < BoardSize.x) && (y >= 0 && y < BoardSize.x))
                {
                    Debug.Log(x+", "+y+", "+tiles[y, x]);
                    SetNewDestination(tiles[y, x]);
                }// return tiles[y,x];
            }
        }

        
    }
    
}
