using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using _18ghostsExam;


public class BoardManager : MonoBehaviour
{
    /// <summary>
    /// Gets the action panel to tell player what to
    /// do at each given moment
    /// </summary>
    public GameObject PlayerActionsPanel;

    /// <summary>
    /// Text in action panel to be changed according
    /// to current game situation
    /// </summary>
    public TextMeshProUGUI PlayerActionsTexts;

    /// <summary>
    /// The text displaying which player is currently
    /// actively moving
    /// </summary>
    public TextMeshProUGUI CurrentPlayerText;

    /// <summary>
    /// Getting the current player
    /// </summary>
    public IPlayer CurrentPlayer;

    private IPlayer PlayerOne;

    private IPlayer PlayerTwo;

    /// <summary>
    /// Getting the prefabs of the tiles
    /// </summary>
    public GameObject[] Pieces;

    public GameObject Mirror;

    public IPortals yellowPortal;

    public IPortals bluePortal;

    public IPortals redPortal;

    /// <summary>
    /// Array of all our ghosts in-game
    /// </summary>
    public Pickable[] Ghosts;

    /// <summary>
    /// Blue portal to instantiate
    /// </summary>
    public GameObject BluePortal;

    /// <summary>
    /// Red portal to instantiate
    /// </summary>
    public GameObject RedPortal;

    /// <summary>
    /// Yellow portal to instantiate
    /// </summary>
    public GameObject YellowPortal;

    /// <summary>
    /// Max number of rows
    /// </summary>
    private const int MaxX = 5;

    /// <summary>
    /// Max number of columns
    /// </summary>
    public const int MaxY = 5;

    /// <summary>
    /// Getting all map elements on the board to set
    /// their positions
    /// </summary>
    public IMapElement[,] positions;

    /// <summary>
    /// Getting the board game object to set everything in a grid inside it
    /// </summary>
    public GameObject BoardObject;


    public string ActionText
    {     
        get
        {
            return
            "It's " + CurrentPlayer.Name + "'s turn!"
            + "\n You can either:" +
            " \n Move a Piece to any adjacent tile that isn't a portal."
            + "\n Fight a ghost if the chamber is occupied (can be your own)." +
            "\n Remove a ghost in the dungeon if you have one, this can only be done" +
            "if there's an available tile matching the colour of your chosen ghost" +
            "and the other player will be the one to set it.";
        }
    }

    

    public string HoldingPieceText
    {
        get
        {
            return
            "holding" + CurrentPlayer.ChosenPiece.colour + " piece";
        }
    }

    public TextMeshProUGUI ActionTextDisplay;

    public GameObject GhostPanel;

    void InitialiseList()
    {
        for(int x = 0; x < MaxX; x++)
        {
            for(int y = 0; y < MaxY; y++)
            {
                if (x == 0)
                {
                    if(y == 0 || y == 3)
                        positions[x, y] = new BlueHall();
                    if (y == 1 || y == 4)
                        positions[x, y] = new RedHall();
                    if (y == 2)
                        positions[x, y] = redPortal;
                }

                if(x == 1)
                {
                    if (y == 0 || y == 2 || y == 4)
                        positions[x, y] = new YellowHall();
                    if (y == 1 || y == 3)
                        positions[x, y] = new Mirror();

                }

                if(x == 2)
                {
                    if (y == 1 || y == 3)
                        positions[x, y] = new BlueHall();
                    if (y == 0 || y == 2)
                        positions[x, y] = new RedHall();
                    if (y == 4)
                        positions[x, y] = yellowPortal;
                }
                if(x == 3)
                {
                    if (y == 0)
                        positions[x, y] = new BlueHall();
                    if (y == 2)
                        positions[x, y] = new YellowHall();
                    if (y == 1 || y == 3)
                        positions[x, y] = new Mirror();
                    if (y == 4)
                        positions[x, y] = new RedHall();

                }
                if(x == 4)
                {
                    if (y == 3)
                        positions[x, y] = new BlueHall();
                    if (y == 1)
                        positions[x, y] = new RedHall();
                    if (y == 0 || y == 4)
                        positions[x, y] = new YellowHall();
                    if (y == 2)
                        positions[x, y] = bluePortal;
                }
            }
        }
    }

    void BoardSetup()
    {
        for (int x = 0; x < MaxX; x++)
        {
            for (int y = 0; y < MaxY; y++)
            {

                if (positions[x, y] is BlueHall)
                {
                    GameObject toInstantiate = Pieces[0];

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IMapElement>();
                }

                if (positions[x, y] is RedHall)
                {
                    GameObject toInstantiate = Pieces[1];

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IMapElement>();
                }

                if (positions[x, y] is YellowHall)
                {
                    GameObject toInstantiate = Pieces[2];

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IMapElement>();
                }

                if (positions[x, y] is Mirror)
                {
                    GameObject toInstantiate = Mirror;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IMapElement>();
                }

                if (positions[x, y] is RedPortals)
                {
                    GameObject toInstantiate = RedPortal;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IPortals>();
                }

                if (positions[x, y] is BluePortals)
                {
                    GameObject toInstantiate = BluePortal;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IPortals>();
                }

                if (positions[x, y] is YellowPortals)
                {
                    GameObject toInstantiate = YellowPortal;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IPortals>();
                }
            }
        }
    }

    
    public void SetUpScene()
    {
        positions = new IMapElement[MaxX, MaxY];
        InitialiseList();
        BoardSetup();

        foreach (IMapElement tile in positions)
        {
            if (!(tile is IPortals))
            {
                Button button = (tile as MonoBehaviour).GetComponent<Button>();
                button.onClick.AddListener(() => PlacePiece(tile));

                //if (Input.GetMouseButtonDown(0))
                //    if (EventSystemManager.currentSystem.IsPointerOverEventSystemObject())
                //        Debug.Log("clicked tile");
                // tile.PlacePiece(CurrentPlayer);
            }
        }
    }


    void Start()
    {
        PlayerOne = new Player();
        PlayerTwo = new Player();

        PlayerOne.Name = "Player A";
        PlayerTwo.Name = "Player B";

        PlayerOne.start = true;

        //CurrentPlayer = GameObject.Find("CurrentPlayer").GetComponent<IPlayer>();

        bluePortal = BluePortal.GetComponent<IPortals>();
        yellowPortal = YellowPortal.GetComponent<IPortals>();
        redPortal = RedPortal.GetComponent<IPortals>();

        SetUpScene();

        CurrentPlayer = PlayerOne;
        CurrentPlayerText.text = CurrentPlayer.Name;    
    }

    /// <summary>
    /// /////////// THIS WORKS SEE LOGIC
    
    public virtual void PickPiece(IGhostBase piece)
    {
        if (piece.inDungeon) //|| piece.inStart)
            Debug.Log("In Dungeon");

        if (!CurrentPlayer.HoldingPiece)
        {
            Debug.Log("picked piece");

            CurrentPlayer.ChosenPiece = piece;

            if (piece.colour == Colours.yellow)
                CurrentPlayer.HoldingYellowPiece = true;
            if (piece.colour == Colours.blue)
                CurrentPlayer.HoldingBluePiece = true;
            if (piece.colour == Colours.red)
                CurrentPlayer.HoldingRedPiece = true;
            CurrentPlayer.HoldingPiece = true;
        }
        //if piece in that position occupied, make it empty
    }


    public virtual void PlacePiece(IMapElement ChosenTile)
    {
        Debug.Log(CurrentPlayer.ChosenPiece.Type);
        Debug.Log(CurrentPlayer.ChosenPiece.OnMirror);

        if (ChosenTile.PieceOnTile == null)
        {
            Debug.Log("move" + CurrentPlayer.ChosenPiece.colour + "to empty"
                + ChosenTile.colour + "piece");

            if (CurrentPlayer.ChosenPiece.OnMirror)
            {
                Debug.Log("onMirror");

                if (ChosenTile.colour == Colours.white)
                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                CurrentPlayer.ChosenPiece.OnMirror = false;
            }

            else if (CurrentPlayer.ChosenPiece.inDungeon)
            {
                Debug.Log("Jailed");

                if (ChosenTile.colour == CurrentPlayer.ChosenPiece.colour)
                {
                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;
                    CurrentPlayer.ChosenPiece.inDungeon = false;
                }
            }

            else
            {
                if (CurrentPlayer.ChosenPiece.colour == Colours.yellow)
                    CurrentPlayer.HoldingYellowPiece = false;

                if (CurrentPlayer.ChosenPiece.colour == Colours.blue)
                    CurrentPlayer.HoldingBluePiece = false;

                if (CurrentPlayer.ChosenPiece.colour == Colours.red)
                    CurrentPlayer.HoldingRedPiece = false;

                if (ChosenTile.colour == Colours.white && !CurrentPlayer.ChosenPiece.OnMirror)
                    CurrentPlayer.ChosenPiece.OnMirror = true;

                (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

            }

            ChosenTile.empty = false;

            ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;
        }

        else
        {
            Debug.Log("occupied piece");

            (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                        (ChosenTile as MonoBehaviour).transform.position;

            CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

            ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;
        }
        CurrentPlayer.HoldingPiece = false;
    }

    void Update()
    {
        
       // Debug.Log(CurrentPlayer.ChosenPiece?.Type);

        CurrentPlayerText.text = CurrentPlayer.Name;

        //Debug.Log(CurrentPlayer.ChosenPiece?.colour);

        if (CurrentPlayer.ChosenPiece != null)
        {
            ActionTextDisplay.text = HoldingPieceText;
        }

        ActionTextDisplay.text = ActionText;

        if (CurrentPlayer.start)
        {
            GhostPanel.SetActive(true);
        }

        /*
        if (CurrentPlayer == PlayerTwo)
            CurrentPlayer = PlayerOne;

        /// INVERT
        if (CurrentPlayer == PlayerTwo)
            CurrentPlayer = PlayerOne;
            */


        foreach (IMapElement position in positions)
        {
            if (position is YellowHall)
            {
                //if (position.PieceOnTile is YellowGhostPickable)
                    //Debug.Log("yeah");
            }
        }

        foreach (IGhostBase ghost in Ghosts)
       {
            if (ghost.GhostDied != null)
            {
                Debug.Log(redPortal.CurrentRot);
                Debug.Log(bluePortal.CurrentRot);
                Debug.Log(yellowPortal.CurrentRot);

                if (ghost.GhostDied.colour == Colours.blue)
                    bluePortal.CurrentRot = bluePortal.Rotate();

                if (ghost.GhostDied.colour == Colours.red)
                    redPortal.CurrentRot = redPortal.Rotate();

                if (ghost.GhostDied.colour == Colours.yellow)
                    yellowPortal.CurrentRot = yellowPortal.Rotate();

                ghost.inDungeon = true;
                ghost.GhostDied = null;
            }
       }
    }
}
