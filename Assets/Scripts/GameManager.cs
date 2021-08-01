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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
