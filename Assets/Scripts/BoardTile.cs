using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    //neighbours
    public BoardTile North=null, South = null, East = null, West = null;
    public BoardTile NextTileOnPath = null;
    int d_Dest; //distance to destination
    public bool hasPath;
    
    //
    public void SetNeighbours(BoardTile n, BoardTile s, BoardTile e, BoardTile w)
    {
        if (n != null) North = n;
        if (s != null) South = s;
        if (e != null) East = e;
        if (w != null) West = w;
    }
    public void SetDestination()
    {
        d_Dest = 0;
        NextTileOnPath = this;
        hasPath = true;
    }

    public void SetNewDistanceToDest(int d)
    {
        d_Dest = d;
    }
    public int GetNewDistanceToDest()
    {
        return d_Dest;
    }

    public void SetNextTileOnPath(BoardTile next)
    {
        NextTileOnPath = next;
        hasPath = true;
    }

    void SetEmpty()
    {
        d_Dest = int.MaxValue;

    }

    public void ClearPath()
    {
        NextTileOnPath = null;
        hasPath = false;
    }

    //UI
    public void ShowPathPointer(int d)
    {
        switch (d)
        {
            case 1:
                PointUp();
                break;
            case 2:
                PointDown();
                break;
            case 3:
                PointLeft();
                break;
            case 4:
                PointRight();
                break;
            
        }
    }
    void PointUp()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0); ;
    }
    void PointDown()
    {
        this.transform.rotation = Quaternion.Euler(0,180,0);
    }
    void PointLeft()
    {
        this.transform.rotation = Quaternion.Euler(0,-90,0);
    }
    void PointRight()
    {
        this.transform.rotation = Quaternion.Euler(0,90,0);
    }
}
