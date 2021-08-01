using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] Transform Ground = default;
    Vector2Int BoardSize;

    public void InitializeGameBoard(Vector2Int Size)
    {
        BoardSize = Size;
        Ground.localScale = new Vector3(Size.x, Size.y, 1);
    }
    
}
