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
        BoardTile board = ClickOnTile();
        if (board)
        {
            //for testing 
            board.gameObject.SetActive(false);
        }
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

    BoardTile ClickOnTile()
    {
        //click to set dest
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int x = (int)(hit.point.x + BoardSize.x * 0.5f);
                int y = (int)(hit.point.z + BoardSize.y * 0.5f);
                if ((x>=0 && x<BoardSize.x) && (y >= 0 && y < BoardSize.x)) return tiles[y,x];
            }
        }

        return null;
    }
    
}
