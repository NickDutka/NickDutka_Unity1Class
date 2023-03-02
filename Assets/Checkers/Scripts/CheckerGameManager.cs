using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckerGameManager : MonoBehaviour
{

    [SerializeField]
    GameObject block;

    [SerializeField]
    GameObject piece;

    bool hasGameFinished, canMove;
    string gameState;

    Player curentPlayer;

    Dictionary<GamePiece, GameObject> pieceDictionary;

    GamePiece clickedPiece;
    
    Board myBoard;

    public static CheckerGameManager instance;

    public delegate void UpdateMessage(Player player, string temp);
    public event UpdateMessage Message;

    // initialize gamemanager and spawn blocks
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SpawnBlocks();

        myBoard = new Board();
        gameState = Constants.CLICK;
        canMove = false;
        hasGameFinished = false;
        curentPlayer = Player.RED;
        pieceDictionary = new Dictionary<GamePiece, GameObject>();

        Dictionary<GamePiece, Grid> posGrid = myBoard.playerPositions;
        foreach (KeyValuePair<GamePiece, Grid> pair in posGrid)
        {
            GameObject pieceObject = Instantiate(piece);
            pieceObject.transform.position = new Vector3(pair.Value.x, -pair.Value.y, -1f);
            pieceObject.GetComponent<SpriteRenderer>().color = pair.Key.player == Player.RED ? Color.red : Color.blue;
            pieceDictionary[pair.Key] = pieceObject;
        }
    }
    // Spawns Blocks on an 8x8 Grid, sets color for every other block black or grey.
    void SpawnBlocks()
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                GameObject temp = Instantiate(block);
                temp.transform.position = new Vector3(i, -j, -0.4f);
                temp.GetComponent<MeshRenderer>().material.color = (i + j) % 2 == 0 ? Color.grey : Color.black; 
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hasGameFinished) return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Grid clickedGrid = new Grid() { x = (int)(mousePos.x + 0.5), y = (int)-(mousePos.y - 0.5) };
            switch (gameState)
            {
                case Constants.CLICK:
                    canMove = false;
                    clickedPiece = myBoard.GetPieceAtGrid(clickedGrid);
                    myBoard.CalculateMoves(curentPlayer);
                    var moveDictionary = myBoard.playerMoves;
                    if(moveDictionary.Count == 0)
                    {
                        hasGameFinished = true;
                    }
                    if (clickedPiece.player != curentPlayer || clickedPiece.pieceNumber == -1) return;

                    foreach(var item in moveDictionary)
                    {
                        if(item.Key == clickedPiece)
                        {
                            canMove = true;
                        }
                    }
                    if (!canMove) return;

                    gameState = Constants.MOVE;

                    break;

                case Constants.MOVE:

                    List<Moves> moves = myBoard.playerMoves[clickedPiece];

                    foreach(Moves currentMove in moves)
                    {
                        if(currentMove.end.x == clickedGrid.x && currentMove.end.y == clickedGrid.y)
                        {
                            pieceDictionary[clickedPiece].transform.position = new Vector3(clickedGrid.x, -clickedGrid.y, -2f);
                            myBoard.UpdateMove(currentMove);
                            gameState = Constants.CLICK;
                            curentPlayer = curentPlayer == Player.RED ? Player.BLUE : Player.RED;
                            return;
                        }
                    }

                    break;

                default:
                    break;

            }
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
