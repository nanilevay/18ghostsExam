using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//using _18ghostsExam;
using System.Linq;

namespace _18ghostsExam
{
    /// <summary>
    /// This class will serve as the main gameloop for our game, and control the
    /// board during each turn
    /// </summary>
    public class BoardManager : MonoBehaviour, IGameController
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
        public IPlayer CurrentPlayer { get; set; }

        public IPlayer PlayerOne { get; set; }

        public IPlayer PlayerTwo { get; set; }

        /// <summary>
        /// For getting the Player's prefabs
        /// </summary>
        public GameObject playerOne;

        public GameObject playerTwo;

        /// <summary>
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase DeadGhost;

        /// <summary>
        /// Getting the board script
        /// </summary>
        public GameBoard board { get; set; }

        /// <summary>
        /// Getting the board prefab
        /// </summary>
        public GameBoard Board;

        /// <summary>
        /// The tile where a ghost was previously
        /// </summary>
        public IMapElement PreviousTile { get; set; }

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

        /// <summary>
        /// For printing texts
        /// </summary>
        public Texts text;

        /// <summary>
        /// This method allows us to set the Current level's initial state
        /// </summary>
        public void Start()
        {
            // Get player's script
            PlayerOne = playerOne.GetComponent<IPlayer>();

            // Get player's script
            PlayerTwo = playerTwo.GetComponent<IPlayer>();

            // Get board's script
            board = Board.GetComponent<GameBoard>();

            // Set player names
            PlayerOne.Name = "Player A";
            PlayerTwo.Name = "Player B";

            // Set player ghosts
            PlayerOne.SetGhosts();
            PlayerTwo.SetGhosts();

            // Set players' state to place initial pieces
            PlayerOne.start = true;
            PlayerTwo.start = true;

            // Define new escaped ghost list
            PlayerOne.EscapedGhosts = new List<IGhostBase>();
            PlayerTwo.EscapedGhosts = new List<IGhostBase>();

            // Set up the board
            board.SetUpScene();

            // Assign buttons to each clickable element to be called in this class
            foreach (IMapElement tile in board.positions)
            {
                if (!(tile is IPortals))
                {
                    Button button = (tile as MonoBehaviour).GetComponent<Button>();
                    button.onClick.AddListener(() => PlacePiece(tile));
                }
            }

            // Assign buttons to each pickable element to be called in this class
            foreach (IGhostBase ghost in PlayerOne.Ghosts)
            {
                Button button = (ghost as MonoBehaviour).GetComponent<Button>();

                button.onClick.AddListener(() => PickPiece
                ((ghost as MonoBehaviour).GetComponent<IGhostBase>()));
            }

            // Assign buttons to each clickable element to be called in this class
            foreach (IGhostBase ghost in PlayerTwo.Ghosts)
            {
                Button button = (ghost as MonoBehaviour).GetComponent<Button>();

                button.onClick.AddListener(() => PickPiece
                ((ghost as MonoBehaviour).GetComponent<IGhostBase>()));
            }

            // Set first player
            CurrentPlayer = PlayerOne;

            // Instantiate texts 
            text = Texts.Instance();

            // Display player name
            CurrentPlayerText.text = PlayerOne.Name;

            // Display possible actions text
            PlayerActionsTexts.text = text.ActionText(CurrentPlayer.Name);
        }


        /// <summary>
        /// This method allows us to change the current player
        /// </summary>
        public void Play()
        {
            // Check if current player is player A
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

            // Check if current player is player B
            else if (CurrentPlayer == PlayerTwo)
            {
                // Change current player name display
                CurrentPlayerText.text = PlayerOne.Name;

                // Change current player
                CurrentPlayer = PlayerOne;

                // Change action text
                PlayerActionsTexts.text = text.ActionText(CurrentPlayer.Name);

                // Count to check how many ghosts have been placed
                counter++;

                // For new checking
                PlayDone = false;
            }
        }


        /// <summary>
        /// This method allows us to pick a piece from the board and get its info
        /// </summary>
        /// <param name="piece">piece being selected by player</param>
        public void PickPiece(IGhostBase piece)
        {
            // Check if piece belongs to current player
            if (CurrentPlayer.Ghosts.Contains(piece))
            {
                // Set piece as chosen piece for moving
                CurrentPlayer.ChosenPiece = piece;

                // Change action text to holding text
                PlayerActionsTexts.text =
                    text.HoldingPieceText(CurrentPlayer.ChosenPiece);

                // If the player isn't at the start
                if (!CurrentPlayer.start)
                {
                    // If the player isn't at the dungeon
                    if (!piece.inDungeon)
                    {
                        // Set previous tile for checking on place
                        PreviousTile = (piece as MonoBehaviour).transform.parent.
                            gameObject.GetComponent<IMapElement>();

                        PreviousTile.PieceOnTile = null;
                    }
                }
            }

            // If the ghost belongs to the other player
            else
            {
                if (CurrentPlayer.ChosenPiece != null)
                {
                    CurrentPlayer.ChosenPiece = null;

                    PlayerActionsTexts.text = text.WrongTurn();
                }
            }
        }

        /// <summary>
        /// This method allows us to check if a move is valid on the board
        /// </summary>
        /// <param name="NextTile"></param>
        /// <returns></returns>
        public bool ValidMove(IMapElement NextTile)
        {
            // If player isn't in dungeon
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

            // Default to false
            return false;
        }

        /// <summary>
        /// This method allows us to place a piece on the board according to the
        /// rules
        /// </summary>
        /// <param name="ChosenTile"></param>
        public virtual void PlacePiece(IMapElement ChosenTile)
        {
            // If the move is on an adjacent tile
            CurrentMoveIsValid = ValidMove(ChosenTile);

            // If the piece on tile is null, no need to fight
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

                // If none of the movements were verified
                else
                    PlayerActionsTexts.text = text.DungeonErrorText();

            }
        }

        /// <summary>
        /// This method allows us to send a ghost to the dungeon if they've lost
        /// </summary>
        /// <param name="dungeonGhost"></param>
        public void SendToDungeon(IGhostBase dungeonGhost)
        {
            // Check empty slot
            foreach (DungeonSlot slot in board.DungeonSlots)
            {
                // Change ghost's position to slot
                if (slot.empty == true)
                {
                    (dungeonGhost as MonoBehaviour).transform.position =
                        (slot as MonoBehaviour).transform.position;
                    (dungeonGhost as MonoBehaviour).transform.parent
                            = (slot as MonoBehaviour).transform;

                    // Change slot's status to filled
                    slot.empty = false;

                    // break out of loop so only one slot is filled
                    break;
                }
            }
            dungeonGhost.inDungeon = true;
            dungeonGhost.inDungeon = true;
        }

        /// <summary>
        /// This method allows us to check if the player has won the game by
        /// having at least one of each ghost colour in their "escaped" list
        /// </summary>
        public void WinCheck()
        {
            // Check surroundings of red portal
            if (board.CheckRedSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());

            // Check surroundings of blue portal
            if (board.CheckBlueSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());

            // Check surroundings of yellow portal
            if (board.CheckYellowSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());

            // If condition met, print winning message
            if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
                PlayerActionsTexts.text = text.VictoryText(CurrentPlayer);
        }

        /// <summary>
        /// The main loop method for the game
        /// </summary>
        public void Update()
        {
            // Check if the player has finished a play and switch
            if (PlayDone)
                Play();

            // To go from the placing of tiles to actual game
            if (counter >= 18)
            {
                PlayerOne.start = false;
                PlayerTwo.start = false;
            }

            // Check if a player has won the game
            WinCheck();

            // If a ghost died, send it to the dungeon
            if (DeadGhost != null)
            {
                SendToDungeon(DeadGhost);

                // If ghost is blue rotate blue portal
                if (DeadGhost is BlueGhostPickable)
                    board.bluePortal.CurrentRot = board.bluePortal.Rotate();

                // If ghost is red rotate red portal
                if (DeadGhost is RedGhostPickable)
                    board.redPortal.CurrentRot = board.redPortal.Rotate();

                // If ghost is yellow rotate yellow portal
                if (DeadGhost is YellowGhostPickable)
                    board.yellowPortal.CurrentRot = board.yellowPortal.Rotate();

                // Set ghost's dungeon status to true
                DeadGhost.inDungeon = true;

                // Reset null ghost for next death
                DeadGhost = null;
            }
        }
    }
}