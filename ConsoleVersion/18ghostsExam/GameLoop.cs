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

        /// <summary>
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase deadGhost;

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
                CurrentPlayer = PlayerOne;
                counter++;
                PlayDone = false;
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

            foreach (IGhostBase ghost in PlayerOne.StartGhosts)
                ghost.character = (char)Characters.ghost;

            foreach (IGhostBase ghost in PlayerTwo.StartGhosts)
                ghost.character = (char)Characters.ghost1;

            board.SetUpScene();

            CurrentPlayer = PlayerOne;

            Texts text = Texts.Instance();

            Console.WriteLine(text.IntroText());

            Console.ReadLine();

            FirstPlay();
        }

        public void FirstPlay()
        {
            for (int i = 0; i < 2; i++)
            {
                board.BoardSetUp();

                int a = 0;

                foreach (IGhostBase ghost in CurrentPlayer.StartGhosts)
                {
                    Console.Write("ghost #" + a + ": ");
                    if (ghost is BlueGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (ghost is RedGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (ghost is YellowGhostPickable)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(ghost.character);
                    Console.ResetColor();
                    a++;
                }

                PlaceStartGhosts();
                CurrentPlayer = PlayerTwo;
                counter++;
            }

            CurrentPlayer = PlayerOne;

            Loop();
        }

        public void MirrorTeleport()
        { }


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
                    Console.WriteLine
               (CurrentPlayer.Name + " , Make your move! " +
               "You can either:");
                    Console.WriteLine
                        ("- Move a ghost to any adjacent tile " +
                        "(besides portals) (enter)");
                    Console.WriteLine
                        ("- Remove a ghost from the dungeon " +
                        "(other player places it (r)");
                    Console.WriteLine
                        ("- Fight a ghost in another tile by " +
                        "moving to its position (enter)");

                    Console.WriteLine("What will you do?");

                    if (Console.ReadLine() == "r")
                        PickFromDungeon();
                    else
                        MoveGhosts();
                }
                

                if (PlayDone)
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
                    Finish = true;
                
                if (deadGhost != null)
                {
                    SendToDungeon(deadGhost);

                    if (deadGhost is BlueGhostPickable)
                        board.bluePortal.CurrentRot = board.bluePortal.Rotate();

                    if (deadGhost is RedGhostPickable)
                        board.redPortal.CurrentRot = board.redPortal.Rotate();

                    if (deadGhost is YellowGhostPickable)
                       board.yellowPortal.CurrentRot = board.yellowPortal.Rotate();

                    deadGhost.inDungeon = true;
                    deadGhost = null;
                }
                
                //Console.Clear();
            }
        }

        public void PickFromDungeon()
        {
            Console.WriteLine("What ghost do you wish to remove?");

            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            int validInputOne;
            int validInputTwo;

            bool InputIsValidOne = Int32.TryParse(inputSplit[0], out validInputOne);
            bool InputIsValidTwo = Int32.TryParse(inputSplit[1], out validInputTwo);

            if (InputIsValidOne && InputIsValidTwo)
            {
                CurrentPlayer.ChosenPiece = board.DungeonSlots
                    [validInputOne, validInputTwo].GhostInSlot;
                
                board.DungeonSlots[validInputOne, validInputTwo].empty = true;

                board.DungeonSlots[validInputOne, validInputTwo].GhostInSlot = null;

                Console.WriteLine("Where to place it?");

                string input2 = Console.ReadLine();

                string[] inputSplit2 = input.Split(",");

                int validInputOne2;
                int validInputTwo2;

                bool InputIsValidOne2 = Int32.TryParse(inputSplit2[0], out validInputOne2);
                bool InputIsValidTwo2 = Int32.TryParse(inputSplit2[1], out validInputTwo2);

                if(InputIsValidOne2 && InputIsValidTwo2)
                    PlacePiece(board.positions[validInputOne2, validInputTwo2]);
                else
                    PickFromDungeon();
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
                    PreviousTile = board.positions
                        [CurrentPlayer.ChosenPiece.Pos.X, 
                        CurrentPlayer.ChosenPiece.Pos.Y];
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
            ///
            /// if move valid
            ///     if occupied fight right away
            ///     if dungeon
            ///     if mirror
            ///     else
            ///
            CurrentMoveIsValid = ValidMove(ChosenTile);

            //if(CurrentMoveIsValid)
            //{
                if (!(ChosenTile.PieceOnTile == null))
                {
                    DeadGhost =
                            CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

                    if (!(DeadGhost == CurrentPlayer.ChosenPiece))
                    {
                        deadGhost = ChosenTile.PieceOnTile;
                        
                        PreviousTile.PieceOnTile = null;
                        
                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        //CurrentPlayer.ChosenPiece.OnMirror = true;

                        //CurrentPlayer.ChosenPiece = null;

                        PlayDone = true;

                        PreviousTile = ChosenTile;                 
                    }

                    else
                    {
                     
                        deadGhost = CurrentPlayer.ChosenPiece;

                        PlayDone = true;
                    }
                }

                    if (CurrentPlayer.ChosenPiece.inDungeon || CurrentPlayer.start)
                {
                    if (ChosenTile.colour == CurrentPlayer.ChosenPiece.colour)
                    {
                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        CurrentPlayer.ChosenPiece.inDungeon = false;

                        Console.WriteLine("placed " + CurrentPlayer.ChosenPiece +
                            "on" + ChosenTile.colour + "tile");

                        PlayDone = true;

                        PreviousTile = ChosenTile;
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

                if (ChosenTile is Mirror &&
                    !CurrentPlayer.ChosenPiece.OnMirror)
                {
                    PreviousTile.PieceOnTile = null;

                    CurrentPlayer.ChosenPiece.OnMirror = true;

                    Console.WriteLine("You're on a mirror! You can" +
                        "teleport.");

                    PlacePiece(ChosenTile);

                    ChosenTile.PieceOnTile = null;
                }

                else
            {
                PreviousTile.PieceOnTile = null;

                ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                
                PlayDone = true;
            }
           // }

            //if (CurrentPlayer.ChosenPiece.OnMirror)
            //{
            //    if (ChosenTile is Mirror)
            //    {
            //        PreviousTile.PieceOnTile = null;

            //        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

            //        CurrentPlayer.ChosenPiece.OnMirror = false;

            //        CurrentPlayer.ChosenPiece = null;

            //        PlayDone = true;
            //    }

            //    else
            //    {
            //        Console.WriteLine("You have to move to another mirror!");

            //        CurrentPlayer.ChosenPiece = null;

            //        PreviousTile = null;
            //    }
            //}
        }

        public void SendToDungeon(IGhostBase dungeonGhost)
        {
            foreach (DungeonSlot slot in board.DungeonSlots)
            {
                if (slot.GhostInSlot == null)
                {
                    slot.GhostInSlot = dungeonGhost;
                    slot.empty = false;
                    dungeonGhost.inDungeon = true;
                    break;
                }
            }          
        }

        void CheckRedSurrounding()
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

        void CheckYellowSurrounding()
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

        void CheckBlueSurrounding()
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


        /*
        IMapElement InputProcess()
        {

        }
        */
        IMapElement inputProcess()
        {
            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            int validInputOne;
            int validInputTwo;

            bool InputIsValidOne = Int32.TryParse(inputSplit[0], out validInputOne);
            bool InputIsValidTwo = Int32.TryParse(inputSplit[1], out validInputTwo);

            if (InputIsValidOne && InputIsValidTwo)
                return board.positions[validInputOne, validInputTwo];

            else
                return null;

        }

        public void MoveGhosts()
        {        
            Console.WriteLine("Write the (x,y) coordinates to select piece");

            if (PreviousTile != null)
                  PreviousTile = inputProcess();

            else
                MoveGhosts();

            if (CurrentPlayer.Ghosts.Contains(PreviousTile.PieceOnTile))
            {          
                CurrentPlayer.ChosenPiece = PreviousTile.PieceOnTile;             
            }

            Console.WriteLine("Selected" + CurrentPlayer.ChosenPiece.colour + "ghost");

            Console.WriteLine("Input coordinates to move to");

            string newinput = Console.ReadLine();

            string[] newinputSplit = newinput.Split(",");

            int newvalidInputOne;
            int newvalidInputTwo;

            bool newInputIsValidOne = 
                Int32.TryParse(newinputSplit[0], out newvalidInputOne);
            bool newInputIsValidTwo = 
                Int32.TryParse(newinputSplit[1], out newvalidInputTwo);

            IMapElement NextTile = 
                board.positions[newvalidInputOne, newvalidInputTwo];

            if (newInputIsValidOne && newInputIsValidTwo)
                PlacePiece(NextTile);
            else
                MoveGhosts();
        }
    }
}