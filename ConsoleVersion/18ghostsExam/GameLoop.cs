using System;
using System.Collections.Generic;
using System.Linq;

namespace _18ghostsExam
{
    /// <summary>
    /// this class serves as the main gameloop for our console based game
    /// </summary>
    public class GameLoop
    {
        /// <summary>
        /// Getting the current player
        /// </summary>
        public IPlayer CurrentPlayer;

        private IPlayer PlayerOne;

        private IPlayer PlayerTwo;

        /// <summary>
        /// To get the prefab of the yellow portal
        /// </summary>
        public IPortals yellowPortal;

        /// <summary>
        /// To get the prefab of the blue portal
        /// </summary>
        public IPortals bluePortal;

        /// <summary>
        /// To get the prefab of the red portal
        /// </summary>
        public IPortals redPortal;

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
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase DeadGhost;

        /// <summary>
        /// Getting a list of the slots in the dungeon for their positions
        /// </summary>
        public DungeonSlot[,] DungeonSlots;

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

        /// <summary>
        /// Text displayed to the players to show which actions are valid
        /// </summary>
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

        /// <summary>
        /// When a player is holding a piece
        /// </summary>
        public string HoldingPieceText
        {
            get
            {
                return
                "holding " + CurrentPlayer.ChosenPiece.colour + " piece";
            }
        }

        /// <summary>
        /// This method allows us to set each player's ghosts list
        /// </summary>
        void SetPlayerGhosts()
        {
            
            for (int i = 0; i < 3; i++)
            {
                BlueGhostPickable instantiateBlueGhost = new BlueGhostPickable();

                PlayerOne.Ghosts.Add(instantiateBlueGhost);

                RedGhostPickable instantiateRedGhost = new RedGhostPickable();

                PlayerOne.Ghosts.Add(instantiateRedGhost);

                YellowGhostPickable instantiateYellowGhost = new YellowGhostPickable();

                PlayerOne.Ghosts.Add(instantiateYellowGhost);

            }


            for (int i = 0; i < 3; i++)
            {
                BlueGhostPickable instantiateBlueGhost = new BlueGhostPickable();

                PlayerTwo.Ghosts.Add(instantiateBlueGhost);

                RedGhostPickable instantiateRedGhost = new RedGhostPickable();

                PlayerTwo.Ghosts.Add(instantiateRedGhost);

                YellowGhostPickable instantiateYellowGhost = new YellowGhostPickable();

                PlayerTwo.Ghosts.Add(instantiateYellowGhost);
            }          
        }

        /// <summary>
        /// This method allows us to set the main map with all the given positions
        /// </summary>
        void InitialiseList()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (x == 0)
                    {
                        if (y == 0 || y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 1 || y == 4)
                            positions[x, y] = new RedHall();
                        if (y == 2)
                            positions[x, y] = new RedPortals();
                    }

                    if (x == 1)
                    {
                        if (y == 0 || y == 2 || y == 4)
                            positions[x, y] = new YellowHall();
                        if (y == 1 || y == 3)
                            positions[x, y] = new Mirror();

                    }

                    if (x == 2)
                    {
                        if (y == 1 || y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 0 || y == 2)
                            positions[x, y] = new RedHall();
                        if (y == 4)
                            positions[x, y] = new YellowPortals();
                    }

                    if (x == 3)
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

                    if (x == 4)
                    {
                        if (y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 1)
                            positions[x, y] = new RedHall();
                        if (y == 0 || y == 4)
                            positions[x, y] = new YellowHall();
                        if (y == 2)
                            positions[x, y] = new BluePortals();
                    }

                    positions[x, y].Pos = new Positions(x, y);
                }
            }
        }

        /// <summary>
        /// This method allows us to set up the board by defining each tile in 
        /// its specified position for a new game according to its type
        /// </summary>
        void BoardSetup()
        {  
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (positions[x, y] is BlueHall)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (positions[x, y] is RedHall)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (positions[x, y] is YellowHall)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    if (positions[x, y] is Mirror)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (positions[x, y] is RedPortals)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (positions[x, y] is BluePortals)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (positions[x, y] is YellowPortals)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    positions[x, y].Pos = new Positions(x, y);

                    Console.Write(positions[x, y].Character);

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            
        }
        /// <summary>
        /// This void allows us to set up the dungeon for when ghosts die
        /// </summary>
        void SetupDungeon()
        {
            /*
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
            */
        }

        public void SetUpScene()
        {
            positions = new IMapElement[MaxX, MaxY];
            InitialiseList();
            BoardSetup();
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
                //CurrentPlayerText.text = PlayerTwo.Name;

                // Change current player
                CurrentPlayer = PlayerTwo;

                // Change action text
                //PlayerActionsTexts.text = ActionText;

                // Count to check how many ghosts have been placed
                counter++;

                // For new checking
                PlayDone = false;
            }

            else if (CurrentPlayer == PlayerTwo)
            {
                //if (TwoHandicap)
                //{

                //CurrentPlayerText.text = PlayerOne.Name;
                CurrentPlayer = PlayerOne;
                //PlayerActionsTexts.text = ActionText;
                counter++;
                PlayDone = false;
                //}

                //TwoHandicap = false;

            }

        }

        /// <summary>
        /// This method allows us to set the Current level's initial state
        /// </summary>
        public void Start()
        {
            PreviousTile = null;

            PlayerOne = new Player();
            PlayerTwo = new Player();

            PlayerOne.Name = "Player A";
            PlayerTwo.Name = "Player B";

            PlayerOne.start = true;

            PlayerTwo.start = true;

            PlayerOne.EscapedGhosts = new List<IGhostBase>();

            PlayerTwo.EscapedGhosts = new List<IGhostBase>();

            DungeonSlots = new DungeonSlot[2, 10];
            SetupDungeon();

            SetUpScene();

            PlayerOne.Ghosts = new List<IGhostBase>();

            PlayerTwo.Ghosts = new List<IGhostBase>();

            SetPlayerGhosts();
            
            CurrentPlayer = PlayerOne;

            Loop();
            
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

                if (!CurrentPlayer.start)
                {
                    if (!piece.inDungeon)
                    {
                        PreviousTile = positions[piece.Pos.X, piece.Pos.Y];      
                        PreviousTile.PieceOnTile = null;
                    }
                }
            }

            else
            {
                if (CurrentPlayer.ChosenPiece != null)
                {
                    CurrentPlayer.ChosenPiece = null;
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
            /*
            CurrentMoveIsValid = ValidMove(ChosenTile);

            if (ChosenTile.PieceOnTile == null)
            {
                if (CurrentPlayer.ChosenPiece.OnMirror)
                {
                    if (ChosenTile is Mirror)
                    {
                        
                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform
                            .position = (ChosenTile as MonoBehaviour).transform.
                            position;

                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                            = (ChosenTile as MonoBehaviour).transform;
                            
                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        CurrentPlayer.ChosenPiece.OnMirror = false;

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
                        //PlayerActionsTexts.text = "Move to a tile of your colour!";

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

                            //PlayerActionsTexts.text = "placed"
                            +ChosenTile.PieceOnTile.colour +
                            "piece on " + ChosenTile.colour +
                            "tile (" + ChosenTile.Pos.X +
                            "," + ChosenTile.Pos.Y + ")";

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
                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (ChosenTile as MonoBehaviour).transform.position;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.parent
                                = (ChosenTile as MonoBehaviour).transform;

                    DeadGhost = CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;


                    PlayDone = true;
                }

                else
                    Debug.Log("invalid move!");

            }
            */
        }

        public void SendToDungeon(IGhostBase dungeonGhost)
        {
            /*
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
            */
        }

        void CheckRedSurrounding()
        {
            if (positions[0, 1].PieceOnTile is RedGhostPickable &&
               redPortal.CurrentRot == PortalDir.left)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }

            if (positions[0, 3].PieceOnTile is BlueGhostPickable &&
                redPortal.CurrentRot == PortalDir.right)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }

            if (positions[1, 2].PieceOnTile is RedGhostPickable &&
                redPortal.CurrentRot == PortalDir.down)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }
        }


        void CheckYellowSurrounding()
        {
            if (positions[1, 4].PieceOnTile is YellowGhostPickable &&
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
            if (positions[4, 2].PieceOnTile is BlueGhostPickable &&
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

       public void Loop()
        {

            if (PlayDone)
                Play();

            if (counter >= 18)
            {
                PlayerOne.start = false;
                PlayerTwo.start = false;
            }


            //CheckYellowSurrounding();
            //CheckRedSurrounding();
            //CheckBlueSurrounding();

            /*
            if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
                Console.WriteLine("CONGRATS");
                */
            /*
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
            */
        }
    }
}