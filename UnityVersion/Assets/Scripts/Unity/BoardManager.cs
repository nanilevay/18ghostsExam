using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using _18ghostsExam;
using System.Linq;


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
    public GameObject[] GhostsP1;

    /// <summary>
    /// Array of all our ghosts in-game
    /// </summary>
    public GameObject[] GhostsP2;

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

    public GameObject DungeonPanel;

    public GameObject DungeonSlot;

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

    public IGhostBase DeadGhost;

    public DungeonSlot[,] DungeonSlots;

    public IMapElement PreviousTile;

    private bool PlayDone = false;

    public GameObject PlayerOnePanel;

    public GameObject PlayerTwoPanel;

    private int counter;

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
            "holding " + CurrentPlayer.ChosenPiece.colour + " piece";
        }
    }

    void SetPlayerGhosts()
    {
        
        for (int i = 0; i < 3; i++)
        {
            GameObject instantiateBlueGhost = GhostsP1[0];

            GameObject instanceBlue =
                Instantiate(instantiateBlueGhost) as GameObject;

            instanceBlue.transform.SetParent(PlayerOnePanel.transform);        

            Button buttonBlue = instanceBlue.GetComponent<Button>();
            
            buttonBlue.onClick.AddListener(() => PickPiece
            (instanceBlue.GetComponent<IGhostBase>()));

            PlayerOne.Ghosts.Add(instanceBlue.GetComponent<IGhostBase>());

            //

            GameObject instantiateRedGhost = GhostsP1[1];

            GameObject instanceRed =
                Instantiate(instantiateRedGhost) as GameObject;

            instanceRed.transform.SetParent(PlayerOnePanel.transform);

            Button buttonRed = instanceRed.GetComponent<Button>();

            buttonRed.onClick.AddListener(() => PickPiece
            (instanceRed.GetComponent<IGhostBase>()));

            PlayerOne.Ghosts.Add(instanceRed.GetComponent<IGhostBase>());

            //
            GameObject instantiateYellowGhost = GhostsP1[2];

            GameObject instanceYellow =
                Instantiate(instantiateYellowGhost) as GameObject;

            instanceYellow.transform.SetParent(PlayerOnePanel.transform);

            Button buttonYellow = instanceYellow.GetComponent<Button>();

            buttonYellow.onClick.AddListener(() => PickPiece
            (instanceYellow.GetComponent<IGhostBase>()));

            PlayerOne.Ghosts.Add(instanceYellow.GetComponent<IGhostBase>());

        }

        ///

        

        for (int i = 0; i < 3; i++)
        {
            GameObject instantiateBlueGhost = GhostsP2[0];

            GameObject instanceBlue =
                Instantiate(instantiateBlueGhost) as GameObject;

            instanceBlue.transform.SetParent(PlayerTwoPanel.transform);

            Button buttonBlue = instanceBlue.GetComponent<Button>();

            buttonBlue.onClick.AddListener(() => PickPiece
            (instanceBlue.GetComponent<IGhostBase>()));

            PlayerTwo.Ghosts.Add(instanceBlue.GetComponent<IGhostBase>());

            //

            GameObject instantiateRedGhost = GhostsP2[1];

            GameObject instanceRed =
                Instantiate(instantiateRedGhost) as GameObject;

            instanceRed.transform.SetParent(PlayerTwoPanel.transform);

            Button buttonRed = instanceRed.GetComponent<Button>();

            buttonRed.onClick.AddListener(() => PickPiece
            (instanceRed.GetComponent<IGhostBase>()));

            PlayerTwo.Ghosts.Add(instanceRed.GetComponent<IGhostBase>());

            //
            GameObject instantiateYellowGhost = GhostsP2[2];

            GameObject instanceYellow =
                Instantiate(instantiateYellowGhost) as GameObject;

            instanceYellow.transform.SetParent(PlayerTwoPanel.transform);

            Button buttonYellow = instanceYellow.GetComponent<Button>();

            buttonYellow.onClick.AddListener(() => PickPiece
            (instanceYellow.GetComponent<IGhostBase>()));

            PlayerTwo.Ghosts.Add(instanceYellow.GetComponent<IGhostBase>());

            foreach (IGhostBase ghost in PlayerTwo.Ghosts)
                Debug.Log("ghost1");
            foreach (IGhostBase ghost in PlayerTwo.Ghosts)
                Debug.Log("ghost2");
        }
    }

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

                    redPortal = instance.GetComponent<IPortals>();
                }

                if (positions[x, y] is BluePortals)
                {
                    GameObject toInstantiate = BluePortal;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IPortals>();

                    bluePortal = instance.GetComponent<IPortals>();
                }

                if (positions[x, y] is YellowPortals)
                {
                    GameObject toInstantiate = YellowPortal;

                    GameObject instance = Instantiate(toInstantiate) as GameObject;

                    instance.transform.SetParent(BoardObject.transform);

                    positions[x, y] = instance.GetComponent<IPortals>();

                    yellowPortal = instance.GetComponent<IPortals>();
                }
            }
        }
    }

    void SetupDungeon()
    {
        for (int a = 0; a < 2; a++)
        {
            for (int b = 0; b < 9; b++)
            {               
                GameObject slotToInstantiate = DungeonSlot;

                GameObject slotInstance = Instantiate(slotToInstantiate) as GameObject;

                slotInstance.transform.SetParent(DungeonPanel.transform);

                DungeonSlots[a, b] = slotInstance.GetComponent<DungeonSlot>();
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
            }
        }
    }

    public void Play()
    {            
            if (CurrentPlayer == PlayerOne)
            {
            Debug.Log("player switch from B to A");
            CurrentPlayerText.text = PlayerTwo.Name;
                CurrentPlayer = PlayerTwo;
                PlayerActionsTexts.text = ActionText;
                counter++;
            PlayDone = false;
        }

            else if (CurrentPlayer == PlayerTwo)
            {
                
                Debug.Log("player switch from A to B");
                CurrentPlayerText.text = PlayerTwo.Name;
                CurrentPlayer = PlayerOne;
                PlayerActionsTexts.text = ActionText;
                counter++;
            PlayDone = false;
        }

        Debug.Log(CurrentPlayer.Name);

        
    }


    void Start()
    {

        PlayerOne = new Player();
        PlayerTwo = new Player();

        PlayerOne.Name = "Player A";
        PlayerTwo.Name = "Player B";

        PlayerOne.start = true;

        PlayerTwo.start = true;

        PlayerOne.EscapedGhosts = new List<IGhostBase>();

        PlayerTwo.EscapedGhosts = new List<IGhostBase>();

        bluePortal = BluePortal.GetComponent<IPortals>();
        yellowPortal = YellowPortal.GetComponent<IPortals>();
        redPortal = RedPortal.GetComponent<IPortals>();

        DungeonSlots = new DungeonSlot[2, 10];
        SetupDungeon();

        SetUpScene();

        PlayerOne.Ghosts = new List<IGhostBase>();

        PlayerTwo.Ghosts = new List<IGhostBase>();


        SetPlayerGhosts();
        
        CurrentPlayer = PlayerOne;
        CurrentPlayerText.text = PlayerOne.Name;

        PlayerActionsTexts.text = ActionText;
    }

    public virtual void PickPiece(IGhostBase piece)
    {
        if (CurrentPlayer.Ghosts.Contains(piece))
        {
            if (!CurrentPlayer.HoldingPiece)
            {
                CurrentPlayer.ChosenPiece = piece;

                CurrentPlayer.HoldingPiece = true;

                PlayerActionsTexts.text = HoldingPieceText;
            }
        }

        else
        {
            
            if (!CurrentPlayer.HoldingPiece)
            {
                CurrentPlayer.ChosenPiece = null;

                CurrentPlayer.HoldingPiece = false;

                PlayerActionsTexts.text = "not your turn!";
            }
        }
    }

    public virtual void PlacePiece(IMapElement ChosenTile)
    {
        Debug.Log(CurrentPlayer.Name + "placed piece");

        if (ChosenTile.PieceOnTile == null)
        {
            if (CurrentPlayer.ChosenPiece.OnMirror)
            {
                if (ChosenTile is Mirror)
                {

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform
                        .position = (ChosenTile as MonoBehaviour).transform.
                        position;

                    //(CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                    //    = (ChosenTile as MonoBehaviour).transform;


                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;
                    
                    CurrentPlayer.ChosenPiece.OnMirror = false;

                    ChosenTile.empty = false;

                    CurrentPlayer.HoldingPiece = false;

                    PlayDone = true;
                }

                else
                    PlayerActionsTexts.text = 
                        "You have to move to another mirror!";             
            }

            else if (CurrentPlayer.ChosenPiece.inDungeon || CurrentPlayer.start)
            {

                if (ChosenTile.colour == CurrentPlayer.ChosenPiece.colour)
                {
                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                        = (ChosenTile as MonoBehaviour).transform;


                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;
                    
                    CurrentPlayer.ChosenPiece.inDungeon = false;

                    ChosenTile.empty = false;

                    CurrentPlayer.HoldingPiece = false;

                    PlayDone = true;
                }

                else
                    PlayerActionsTexts.text = "Move to a tile of your colour!";
            }

            else
            {

                if (ChosenTile is Mirror &&
                    !CurrentPlayer.ChosenPiece.OnMirror)
                {
                    CurrentPlayer.ChosenPiece.OnMirror = true;
                    PlayerActionsTexts.text = "You're on a mirror! You can" +
                        "teleport.";
                }


                (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                        = (ChosenTile as MonoBehaviour).transform;

                ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                ChosenTile.empty = false;

                CurrentPlayer.HoldingPiece = false;

                PlayDone = true;
            }
        }

        else
        {

            (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                        (ChosenTile as MonoBehaviour).transform.position;

            (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                        = (ChosenTile as MonoBehaviour).transform;

            DeadGhost = CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

            ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

            CurrentPlayer.HoldingPiece = false;

            PlayDone = true;

            
        }
    }

    public void SendToDungeon(IGhostBase dungeonGhost)
    {
        foreach (DungeonSlot slot in DungeonSlots)
        {
            if (slot.empty == true)
            {
                (dungeonGhost as MonoBehaviour).transform.position = 
                    (slot as MonoBehaviour).transform.position;
                (dungeonGhost as MonoBehaviour).transform.parent
                        = (slot as MonoBehaviour).transform;
                slot.empty = false;
                break;
            }
        }
        dungeonGhost.inDungeon = true;
        dungeonGhost.inDungeon = true;
    }

    void CheckRedSurrounding()
    {
        if (positions[0,1].PieceOnTile is RedGhostPickable &&
           redPortal.CurrentRot == PortalDir.left)
        {
            CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
        }

        if (positions[0, 3].PieceOnTile is BlueGhostPickable &&
            redPortal.CurrentRot == PortalDir.right)
        {
            CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
        }

        if  (positions[1, 2].PieceOnTile is RedGhostPickable &&
            redPortal.CurrentRot == PortalDir.down)
        {
            CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
        }
    }


    void CheckYellowSurrounding()
    {
        if (positions[1,4].PieceOnTile is YellowGhostPickable && 
            yellowPortal.CurrentRot == PortalDir.up)
        {
            CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
        }

        if (positions[2, 3].PieceOnTile is YellowGhostPickable &&
            yellowPortal.CurrentRot == PortalDir.left)
        {
            CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
        }

        if (positions[3, 4].PieceOnTile is YellowGhostPickable &&
            yellowPortal.CurrentRot == PortalDir.down)
        {
            CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
        }
    }

    void CheckBlueSurrounding()
    {
        if (positions[4,2].PieceOnTile is BlueGhostPickable && 
            bluePortal.CurrentRot == PortalDir.left)
        {
            CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
        }

        if (positions[3, 2].PieceOnTile is BlueGhostPickable &&
             bluePortal.CurrentRot == PortalDir.up)
        {
            CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
        }

        if (positions[1, 4].PieceOnTile is BlueGhostPickable &&
            bluePortal.CurrentRot == PortalDir.right)
        {
            CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
        }

    }

    void Update()
    {
        if(PlayDone)
            Play();

        if (counter >= 18)
        {
            PlayerOne.start = false;
            PlayerTwo.start = false;
        }


        CheckYellowSurrounding();
        CheckRedSurrounding();
        CheckBlueSurrounding();

        if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
            && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
            && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
            Debug.Log("YEAAAAAAAAAAH PLAYER" + CurrentPlayer.Name + "WON");


        /*
          foreach (IMapElement position in positions)
        {
            if (position is YellowHall)
            {
                //if (position.PieceOnTile is YellowGhostPickable)
                    //Debug.Log("yeah");
            }
        }
        */

        if (DeadGhost != null)
        {
            SendToDungeon(DeadGhost);

            if (DeadGhost is BlueGhostPickable)
                bluePortal.CurrentRot = bluePortal.Rotate();

            if (DeadGhost is RedGhostPickable)
                redPortal.CurrentRot = redPortal.Rotate();

            if (DeadGhost is YellowGhostPickable)
                yellowPortal.CurrentRot = yellowPortal.Rotate();

            DeadGhost.inDungeon = true;
            DeadGhost = null;
        }   
        
    }
}
