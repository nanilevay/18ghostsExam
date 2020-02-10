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
        public GameBoard board;
        /// <summary>
        /// Getting the current player
        /// </summary>
        public IPlayer CurrentPlayer;

        /// <summary>
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase DeadGhost;

        private IPlayer PlayerOne;

        private IPlayer PlayerTwo;

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

        private bool Finish = false;

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
            PlayerOne = new Player();
            PlayerTwo = new Player();

            board = new GameBoard();

            PlayerOne.Name = "Player A";
            PlayerTwo.Name = "Player B";

            PlayerOne.start = true;

            PlayerTwo.start = true;

            PlayerOne.EscapedGhosts = new List<IGhostBase>();

            PlayerTwo.EscapedGhosts = new List<IGhostBase>();

            board.SetUpScene();

            CurrentPlayer = PlayerOne;

            Texts text = Texts.Instance();
            
            Console.WriteLine(text.IntroText());

            Console.ReadLine();

            Loop();
        }

        public void Loop()
        {
            while (!Finish)
            {
                board.BoardSetUp();

                int i = 0;
          
                foreach (IGhostBase ghost in CurrentPlayer.StartGhosts)
                {
                    Console.Write("ghost #" + i + ": ");
                    if (ghost is BlueGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (ghost is RedGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (ghost is YellowGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(ghost.character);
                    Console.ResetColor();
                    i++;
                }

                if (CurrentPlayer.start)
                    PlaceStartGhosts();

                
                else
                {
                    Console.WriteLine("What will you do?");
                    if (Console.ReadLine() == "r")
                        PickFromDungeon();

                    MoveGhosts();
                }
                

                if (PlayDone)
                    Play();

                
                if (counter >= 18)
                {
                    PlayerOne.start = false;
                    PlayerTwo.start = false;
                }

                //board.CheckYellowSurrounding();
                //board.CheckRedSurrounding();
                //board.CheckBlueSurrounding();

                if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
                    && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
                    && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
                    Finish = true;

                
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
                
                Console.Clear();

            }
        }

        public void PickFromDungeon()
        {
            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            int validInputOne;
            int validInputTwo;

            bool InputIsValidOne = Int32.TryParse(inputSplit[0], out validInputOne);
            bool InputIsValidTwo = Int32.TryParse(inputSplit[1], out validInputTwo);

            if (InputIsValidOne && InputIsValidTwo &&
                CurrentPlayer.Ghosts.Contains
                (board.DungeonSlots[validInputOne, validInputTwo].GhostInSlot))
            {
                board.DungeonSlots[validInputOne, validInputTwo].empty = true;

                CurrentPlayer.ChosenPiece = board.DungeonSlots[validInputOne, validInputTwo].GhostInSlot;

                board.DungeonSlots[validInputOne, validInputTwo].GhostInSlot = null;

                string input2 = Console.ReadLine();

                string[] inputSplit2 = input.Split(",");

                int validInputOne2;
                int validInputTwo2;

                bool InputIsValidOne2 = Int32.TryParse(inputSplit2[0], out validInputOne2);
                bool InputIsValidTwo2 = Int32.TryParse(inputSplit2[1], out validInputTwo2);

                PlacePiece(board.positions[validInputOne2, validInputTwo]);
            }

            else
                PickFromDungeon();
        }

        /// <summary>
        /// This method allows us to pick a piece from the board and get its info
        /// </summary>
        /// <param name="piece">piece being selected by player</param>
        public void PickPiece()
        {
            if (!CurrentPlayer.start)
            {
                if (!CurrentPlayer.ChosenPiece.inDungeon)
                {
                    PreviousTile = board.positions[CurrentPlayer.ChosenPiece.Pos.X, CurrentPlayer.ChosenPiece.Pos.Y];
                    PreviousTile.PieceOnTile = null;
                }
            }

            Console.WriteLine("picked " + CurrentPlayer.ChosenPiece +
                            "ghost");

            Console.WriteLine("Place on a tile of its same colour!");

            Console.WriteLine("Write the (x,y) coordinates");

            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            IMapElement chosenTile = board.positions[Int32.Parse(inputSplit[0]),
                Int32.Parse(inputSplit[1])];

            Console.Write("Ghost: ");
            Console.WriteLine(CurrentPlayer.ChosenPiece.colour);
            Console.Write("Tile: ");
            Console.WriteLine(chosenTile.colour);

            PlacePiece(chosenTile);
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

            if (!(ChosenTile is IPortals))
            {
                if (ChosenTile.PieceOnTile == null)
                {
                    if (CurrentPlayer.ChosenPiece.OnMirror)
                    {
                        if (ChosenTile is Mirror)
                        {
                            PreviousTile.PieceOnTile = null;

                            ChosenTile.Character = CurrentPlayer.ChosenPiece.character;

                            ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                            CurrentPlayer.ChosenPiece.OnMirror = false;

                            PreviousTile = ChosenTile;

                           

                            PlayDone = true;
                        }

                        else
                        {
                            Console.WriteLine("You have to move to another mirror!");

                            CurrentPlayer.ChosenPiece = null;

                            PreviousTile = null;
                        }
                    }

                    else if (CurrentPlayer.ChosenPiece.inDungeon || CurrentPlayer.start)
                    {
                        if (ChosenTile.colour == CurrentPlayer.ChosenPiece.colour)
                        {
                            ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                            CurrentPlayer.ChosenPiece.inDungeon = false;

                            PreviousTile = null;

                            Console.WriteLine("placed " + CurrentPlayer.ChosenPiece +
                                "on" + ChosenTile.colour + "tile");

                            PlayDone = true;
                        }

                        else
                        {
                            Console.WriteLine("Move to a tile of your colour!");
                            Console.WriteLine("tried to place" + CurrentPlayer.ChosenPiece +
                                "on" + ChosenTile.colour + "tile");

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

                                Console.WriteLine("You're on a mirror! You can" +
                                    "teleport.");

                                PlacePiece(ChosenTile);

                                ChosenTile.PieceOnTile = null;

                               // PlayDone = true;
                            }

                            else
                            {
                                PreviousTile.PieceOnTile = null;

                                Console.WriteLine("placed"
                                + ChosenTile.PieceOnTile.colour +
                                "piece on " + ChosenTile.colour +
                                "tile (" + ChosenTile.Pos.X +
                                "," + ChosenTile.Pos.Y + ")");

                                ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                                PreviousTile = ChosenTile;

                                CurrentPlayer.ChosenPiece = null;
                            }
                        }
                    }
                }

                else
                {
                    if (CurrentMoveIsValid)
                    {
                        PreviousTile.PieceOnTile = null;

                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        DeadGhost =
                            CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

                        Console.WriteLine("ghost sent to dungeon");

                        PlayDone = true;

                    }

                    else
                        Console.WriteLine("invalid move!");
                }
            }
            else
            {
                Console.WriteLine("Any tile that isn't a portal!");
            }
        }

        public void SendToDungeon(IGhostBase dungeonGhost)
        {
            foreach (DungeonSlot slot in board.DungeonSlots)
            {
                if (slot.empty == true)
                {
                    slot.GhostInSlot = dungeonGhost;
                    slot.empty = false;
                    break;
                }
            }
            dungeonGhost.inDungeon = true;
        }

        void CheckRedSurrounding(IPlayer CurrentPlayer)
        {
            if (board.positions[0, 1].PieceOnTile is RedGhostPickable &&
               board.redPortal.CurrentRot == PortalDir.left)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }

            if (board.positions[0, 3].PieceOnTile is BlueGhostPickable &&
                board.redPortal.CurrentRot == PortalDir.right)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }

            if (board.positions[1, 2].PieceOnTile is RedGhostPickable &&
                board.redPortal.CurrentRot == PortalDir.down)
            {
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());
            }
        }


        void CheckYellowSurrounding(IPlayer CurrentPlayer)
        {
            if (board.positions[1, 4].PieceOnTile is YellowGhostPickable &&
                board.yellowPortal.CurrentRot == PortalDir.up)
            {
                CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
            }

            if (board.positions[2, 3].PieceOnTile is YellowGhostPickable &&
                board.yellowPortal.CurrentRot == PortalDir.left)
            {
                CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
            }

            if (board.positions[3, 4].PieceOnTile is YellowGhostPickable &&
                board.yellowPortal.CurrentRot == PortalDir.down)
            {
                CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());
            }
        }

        void CheckBlueSurrounding(IPlayer CurrentPlayer)
        {
            if (board.positions[4, 2].PieceOnTile is BlueGhostPickable &&
                board.bluePortal.CurrentRot == PortalDir.left)
            {
                CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
            }

            if (board.positions[3, 2].PieceOnTile is BlueGhostPickable &&
                 board.bluePortal.CurrentRot == PortalDir.up)
            {
                CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
            }

            if (board.positions[1, 4].PieceOnTile is BlueGhostPickable &&
                board.bluePortal.CurrentRot == PortalDir.right)
            {
                CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());
            }
        }

        public void PlaceStartGhosts()
        {
            Console.WriteLine(CurrentPlayer.Name + " , pick one of your ghosts!");

            string input = Console.ReadLine();

            int validInput;

            bool InputIsValid = Int32.TryParse(input, out validInput);

            if (InputIsValid)
            {
                CurrentPlayer.Ghosts.Add(CurrentPlayer.StartGhosts[validInput]);

                CurrentPlayer.ChosenPiece = CurrentPlayer.StartGhosts[validInput];

                Console.WriteLine(CurrentPlayer.ChosenPiece.colour);

                CurrentPlayer.StartGhosts.RemoveAt(validInput);

                PickPiece();
            }

            else
                PlaceStartGhosts();
        }

        public void MoveGhosts()
        {
            Console.WriteLine
                (CurrentPlayer.Name + " , Make your move! You can either:");
            Console.WriteLine
                ("- Move a ghost to any adjacent tile (besides portals)");
            Console.WriteLine
                ("- Remove a ghost from the dungeon (other player places it");
            Console.WriteLine
                ("- Fight a ghost in another tile by moving to its position");

            Console.WriteLine("Write the (x,y) coordinates to select piece");

            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            int validInputOne;
            int validInputTwo;

            bool InputIsValidOne = Int32.TryParse(inputSplit[0], out validInputOne);
            bool InputIsValidTwo = Int32.TryParse(inputSplit[1], out validInputTwo);

            PreviousTile = board.positions[validInputOne, validInputTwo];

            if (InputIsValidOne && InputIsValidTwo && CurrentPlayer.Ghosts.Contains(PreviousTile.PieceOnTile))
            {          
                CurrentPlayer.ChosenPiece = PreviousTile.PieceOnTile;             
            }

            else
                MoveGhosts();

            Console.WriteLine("Selected" + CurrentPlayer.ChosenPiece.colour + "ghost");

            Console.WriteLine("Input coordinates to move to");

            string newinput = Console.ReadLine();

            string[] newinputSplit = newinput.Split(",");

            int newvalidInputOne;
            int newvalidInputTwo;

            bool newInputIsValidOne = Int32.TryParse(newinputSplit[0], out newvalidInputOne);
            bool newInputIsValidTwo = Int32.TryParse(newinputSplit[1], out newvalidInputTwo);

            IMapElement NextTile = board.positions[newvalidInputOne, newvalidInputTwo];

            if (newInputIsValidOne && newInputIsValidTwo)
                PlacePiece(NextTile);
            else
                MoveGhosts();
        }
    }
}