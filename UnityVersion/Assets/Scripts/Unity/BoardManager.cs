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

    public GameObject playerOne;

    public GameObject playerTwo;

    /// <summary>
    /// To check what ghost died to rotate portals
    /// </summary>
    public IGhostBase DeadGhost;

    public IBoardSetup board;

    public GameBoard Board;

    /// <summary>
    /// The tile where a ghost was previously
    /// </summary>
    public IMapElement PreviousTile;

    /// <summary>
    /// If the turn is over, switch the players
    /// </summary>
    private bool PlayDone = false;

    /// <summary>
    /// To count until all ghosts are set
    /// </summary>
    private int counter;

    /// <summary>
    /// To check the order of play at the start
    /// </summary>
    private bool TwoHandicap = true;

    /// <summary>
    /// To check whether a move is valid or not
    /// </summary>
    private bool CurrentMoveIsValid;

    public Texts text;


    /// <summary>
    /// This method allows us to set the Current level's initial state
    /// </summary>
    void Start()
    {
        PlayerOne = playerOne.GetComponent<IPlayer>();

        PlayerTwo = playerTwo.GetComponent<IPlayer>();

        board = Board.GetComponent<GameBoard>();

        PlayerOne.Name = "Player A";
        PlayerTwo.Name = "Player B";

        PlayerOne.SetGhosts();
        PlayerTwo.SetGhosts();

        PlayerOne.start = true;

        PlayerTwo.start = true;

        PlayerOne.EscapedGhosts = new List<IGhostBase>();

        PlayerTwo.EscapedGhosts = new List<IGhostBase>();

        board.SetUpScene();

        foreach (IMapElement tile in board.positions)
        {
            if (!(tile is IPortals))
            {
                Button button = (tile as MonoBehaviour).GetComponent<Button>();
                button.onClick.AddListener(() => PlacePiece(tile));
            }
        }

        foreach(IGhostBase ghost in PlayerOne.Ghosts)
        {
            Button button = (ghost as MonoBehaviour).GetComponent<Button>();

            button.onClick.AddListener(() => PickPiece
            ((ghost as MonoBehaviour).GetComponent<IGhostBase>()));
        }

        foreach (IGhostBase ghost in PlayerTwo.Ghosts)
        {
            Button button = (ghost as MonoBehaviour).GetComponent<Button>();

            button.onClick.AddListener(() => PickPiece
            ((ghost as MonoBehaviour).GetComponent<IGhostBase>()));
        }

        CurrentPlayer = PlayerOne;

        text = Texts.Instance();

        CurrentPlayer = PlayerOne;
        CurrentPlayerText.text = PlayerOne.Name;

        PlayerActionsTexts.text = text.ActionText(CurrentPlayer.Name);
    }


    /// <summary>
    /// This method allows us to change the current player
    /// </summary>
    public void Play()
    {
        // check if current player is player A
        if (CurrentPlayer == PlayerOne)
        {
            // Change current player name display
            CurrentPlayerText.text = PlayerTwo.Name;

            // Change current player
            CurrentPlayer = PlayerTwo;

            // Change action text
            PlayerActionsTexts.text = text.ActionText(CurrentPlayer.Name);

            // Count to check how many ghosts have been placed
            counter++;

            // For new checking
            PlayDone = false;
        }

        else if (CurrentPlayer == PlayerTwo)
        {
            //if (TwoHandicap)
            //{

            CurrentPlayerText.text = PlayerOne.Name;
            CurrentPlayer = PlayerOne;
            PlayerActionsTexts.text = text.ActionText(CurrentPlayer.Name);
            counter++;
            PlayDone = false;
            //}
            //TwoHandicap = false;

        }

    }

  
    /// <summary>
    /// This method allows us to pick a piece from the board and get its info
    /// </summary>
    /// <param name="piece">piece being selected by player</param>
    public virtual void PickPiece(IGhostBase piece)
    {
        // Check if piece belongs to current player
        if (CurrentPlayer.Ghosts.Contains(piece))
        {
            CurrentPlayer.ChosenPiece = piece;

            PlayerActionsTexts.text = text.HoldingPieceText(CurrentPlayer.ChosenPiece);

            if (!CurrentPlayer.start)
            {
                if (!piece.inDungeon)
                {
                    PreviousTile = (piece as MonoBehaviour).transform.parent.
                        gameObject.GetComponent<IMapElement>();

                    PreviousTile.PieceOnTile = null;
                }
            }
        }

        else
        {

            if (CurrentPlayer.ChosenPiece != null)
            {
                CurrentPlayer.ChosenPiece = null;

                PlayerActionsTexts.text = "not your turn!";
            }
        }
    }


    public bool ValidMove(IMapElement NextTile)
    {
        if (PreviousTile != null)
        {

                if (NextTile.Pos.X == PreviousTile.Pos.X + 1
                    && NextTile.Pos.Y == PreviousTile.Pos.Y)
                    return true;
                if (NextTile.Pos.X == PreviousTile.Pos.X - 1
                && NextTile.Pos.Y == PreviousTile.Pos.Y)
                    return true;
                if (NextTile.Pos.X == PreviousTile.Pos.X
                && NextTile.Pos.Y == PreviousTile.Pos.Y + 1)
                    return true;
                if (NextTile.Pos.X == PreviousTile.Pos.X
                && NextTile.Pos.Y == PreviousTile.Pos.Y - 1)
                    return true;
        }

        return false;
    }

    public virtual void PlacePiece(IMapElement ChosenTile)
    {
        CurrentMoveIsValid = ValidMove(ChosenTile);

        if (ChosenTile.PieceOnTile == null)
        {
            if (CurrentPlayer.ChosenPiece.OnMirror)
            {
                if (ChosenTile is Mirror)
                {
                    PreviousTile.PieceOnTile = null;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform
                        .position = (ChosenTile as MonoBehaviour).transform.
                        position;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                        = (ChosenTile as MonoBehaviour).transform;

                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                    CurrentPlayer.ChosenPiece.OnMirror = false;

                    CurrentPlayer.ChosenPiece = null;

                    PlayDone = true;

                    PreviousTile = ChosenTile;
                }

                else
                {
                    PlayerActionsTexts.text =
                        "You have to move to another mirror!";

                    CurrentPlayer.ChosenPiece = null;

                    PreviousTile = null;
                }
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

                    PlayDone = true;

                    PreviousTile = ChosenTile;
                }

                else
                {
                    PlayerActionsTexts.text = "Move to a tile of your colour!";

                    CurrentPlayer.ChosenPiece = null;

                    PreviousTile = null;
                }
            }

            else
            {
                if (CurrentMoveIsValid)
                {
                    if (ChosenTile is Mirror &&
                    !CurrentPlayer.ChosenPiece.OnMirror)
                    {
                        PreviousTile.PieceOnTile = null;

                        CurrentPlayer.ChosenPiece.OnMirror = true;

                        PlayerActionsTexts.text = "You're on a mirror! You can" +
                            "teleport.";

                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                                = (ChosenTile as MonoBehaviour).transform;

                        ChosenTile.PieceOnTile = null;
                    }

                    else
                    {
                        PreviousTile.PieceOnTile = null;

                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                                = (ChosenTile as MonoBehaviour).transform;

                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        PreviousTile = ChosenTile;

                        CurrentPlayer.ChosenPiece = null;

                        PlayDone = true;
                    }
                }
            }
        }

        else
        {
            if (CurrentMoveIsValid)
            {
                if (ChosenTile is Mirror)
                {
                    PreviousTile.PieceOnTile = null;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform
                        .position = (ChosenTile as MonoBehaviour).transform.
                        position;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                        = (ChosenTile as MonoBehaviour).transform;

                    DeadGhost = CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

                    if (!(DeadGhost == CurrentPlayer.ChosenPiece))
                    {
                        PreviousTile.PieceOnTile = null;

                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        CurrentPlayer.ChosenPiece.OnMirror = true;

                        CurrentPlayer.ChosenPiece = null;

                        PlayDone = true;

                        PreviousTile = ChosenTile;
                    }

                    else
                    {
                        CurrentPlayer.ChosenPiece = null;

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

                    if (!(DeadGhost == CurrentPlayer.ChosenPiece))
                    {
                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        CurrentPlayer.ChosenPiece.OnMirror = true;

                        CurrentPlayer.ChosenPiece = null;

                        PlayDone = true;

                        PreviousTile = ChosenTile;
                    }

                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;


                    PlayDone = true;
                }
            }

            else
                Debug.Log("invalid move!");

        }
    }

    public void SendToDungeon(IGhostBase dungeonGhost)
    {
        foreach (DungeonSlot slot in board.DungeonSlots)
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

    void Update()
    {

        if (PlayDone)
            Play();

        if (counter >= 18)
        {
            PlayerOne.start = false;
            PlayerTwo.start = false;
        }     

        if(board.CheckYellowSurrounding())
            CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
          
        if(board.CheckRedSurrounding())
            CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());

        if (board.CheckBlueSurrounding())
            CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());

        if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
            && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
            && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
            Debug.Log("YEAAAAAAAAAAH PLAYER" + CurrentPlayer.Name + "WON");
            
        if (DeadGhost != null)
        {
            SendToDungeon(DeadGhost);

            if (DeadGhost is BlueGhostPickable)
                board.bluePortal.CurrentRot = board.bluePortal.Rotate();

            if (DeadGhost is RedGhostPickable)
                board.redPortal.CurrentRot = board.redPortal.Rotate();

            if (DeadGhost is YellowGhostPickable)
                board.yellowPortal.CurrentRot = board.yellowPortal.Rotate();

            DeadGhost.inDungeon = true;
            DeadGhost = null;
        }

    }
}
