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

    Player currentPlayer;

    Dictionary<GamePiece, GameObject> pieceDictionary;

    GamePiece clickedPiece;

    Board myBoard;

    public static CheckerGameManager instance;

    public delegate void UpdateMessage(Player player, string temp);
    public event UpdateMessage Message;

    private void Awake()
    {
        if (instance == null)
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
        currentPlayer = Player.RED;
        pieceDictionary = new Dictionary<GamePiece, GameObject>();


        Dictionary<GamePiece, Grid> posGrid = myBoard.playerPositions;
        foreach (KeyValuePair<GamePiece, Grid> pair in posGrid)
        {
            GameObject pieceObject = Instantiate(piece);
            pieceObject.transform.position = new Vector3(pair.Value.x, -pair.Value.y, -2f);
            pieceObject.GetComponent<SpriteRenderer>().color = pair.Key.player == Player.RED ? Color.red : Color.blue;
            pieceDictionary[pair.Key] = pieceObject;
        }
    }

    void SpawnBlocks()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject temp = Instantiate(block);
                temp.transform.position = new Vector3(i, -j, -1f);
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
            Vector3 mousePos = Mouse.current.position.ReadValue();
                    mousePos.z = Camera.main.nearClipPlane;
                    Vector3 WorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Grid clickedGrid = new Grid() { x = (int)(WorldPos.x + 0.5), y = (int)-(WorldPos.y - 0.5) };
            switch (gameState)
            {
                case Constants.CLICK:
                    canMove = false;
                    clickedPiece = myBoard.GetPieceAtGrid(clickedGrid);
                    myBoard.CalculateMoves(currentPlayer);
                    var moveDictionary = myBoard.playerMoves;
                    if (moveDictionary.Count == 0)
                    {
                        hasGameFinished = true;
                        Message(currentPlayer, Constants.FINISHED);
                    }
                    if (clickedPiece.player != currentPlayer || clickedPiece.pieceNumber == -1) return;

                    foreach (var item in moveDictionary)
                    {
                        if (item.Key == clickedPiece)
                        {
                            canMove = true;
                        }
                    }

                    if (!canMove) return;
                    Message(currentPlayer, Constants.MOVE);
                    gameState = Constants.MOVE;


                    break;

                case Constants.MOVE:

                    List<Moves> moves = myBoard.playerMoves[clickedPiece];

                    foreach (Moves currentMove in moves)
                    {
                        if (currentMove.end.x == clickedGrid.x && currentMove.end.y == clickedGrid.y)
                        {
                            pieceDictionary[clickedPiece].transform.position = new Vector3(clickedGrid.x, -clickedGrid.y, -2f);
                            if (currentMove.isCapture)
                            {
                                pieceDictionary[currentMove.capturedPiece].SetActive(false);
                                pieceDictionary.Remove(currentMove.capturedPiece);
                            }
                            myBoard.UpdateMove(currentMove);
                            if (currentMove.end.y == 7 && currentPlayer == Player.RED)
                            {
                                myBoard.UpgradePiece(clickedPiece);
                                pieceDictionary[clickedPiece].transform.GetChild(0).gameObject.SetActive(true);
                            }
                            if (currentMove.end.y == 0 && currentPlayer == Player.BLUE)
                            {
                                myBoard.UpgradePiece(clickedPiece);
                                pieceDictionary[clickedPiece].transform.GetChild(0).gameObject.SetActive(true);
                            }

                            gameState = Constants.CLICK;
                            myBoard.CalculateMoves(currentPlayer);
                            if (myBoard.isCapturedMove && currentMove.isCapture)
                            {
                                Message(currentPlayer, Constants.CLICK);
                                return;
                            }
                            currentPlayer = currentPlayer == Player.RED ? Player.BLUE : Player.RED;
                            Message(currentPlayer, Constants.CLICK);
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
