using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    //neighbours
    public BoardTile North=null, South = null, East = null, West = null;
    int d_Dest; //distance to destination


    //
    public void SetNeighbours(BoardTile n, BoardTile s, BoardTile e, BoardTile w)
    {
        if (n != null) North = n;
        if (s != null) South = s;
        if (e != null) East = e;
        if (w != null) West = w;
    }
    void SetDestination()
    {
        d_Dest = 0;
    }

    void SetEmpty()
    {
        d_Dest = int.MaxValue;
    }
}
