using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameBoard gameBoard = default;
    [SerializeField] Vector2Int boardSize = new Vector2Int(10,10);


    private void Awake()
    {
        gameBoard.InitializeGameBoard(boardSize);
    }

    //validate the width and length to be greater than 2
    private void OnValidate()
    {
        if (boardSize.x < 2) boardSize.x = 2;
        if (boardSize.y < 2) boardSize.y = 2;

    }

   
}
