using System;
using System.Collections.Generic;
using System.Linq;

namespace _18ghostsExam
{
    /// <summary>
    /// this class serves as the main gameloop for our console based game
    /// </summary>
    public class GameLoop : IGameController
    {
        /// <summary>
        /// Getting the main board for the game
        /// </summary>
        public GameBoard board { get; set; }
        /// <summary>
        /// Getting the current player
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase DeadGhost;

        /// <summary>
        /// To check what ghost died to rotate portals
        /// </summary>
        public IGhostBase deadGhost;

        /// <summary>
        /// Getting player one
        /// </summary>
        public IPlayer PlayerOne { get; set; }

        /// <summary>
        /// Getting player two
        /// </summary>
        public IPlayer PlayerTwo { get; set; }

        /// <summary>
        /// Checking the previous tile chosen by user
        /// </summary>
        public IMapElement PreviousTile { get; set; }

        /// <summary>
        /// If the turn is over, switch the players
        /// </summary>
        private bool PlayDone = false;

        /// <summary>
        /// Checking the next tile chosen by the user
        /// </summary>
        public IMapElement NextTile { get; set; }

        /// <summary>
        /// To count until all ghosts are set
        /// </summary>
        private int counter;

        /// <summary>
        /// To check whether a move is valid or not
        /// </summary>
        private bool CurrentMoveIsValid;

        /// <summary>
        /// Checking if the game has ended
        /// </summary>
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

                // Count to check how many ghosts have been placed
                counter++;

                // For new checking
                PlayDone = false;
            }

            else if (CurrentPlayer == PlayerTwo)
            {
                // Change current player
                CurrentPlayer = PlayerOne;

                // Count to check how many ghosts have been placed
                counter++;
                // For new checking
                PlayDone = false;
            }
        }

        /// <summary>
        /// This method allows us to set the Current level's initial state
        /// </summary>
        public void Start()
        {
            // Initialise our players
            PlayerOne = new Player();
            PlayerTwo = new Player();

            // Begin our board for setup
            board = new GameBoard();

            PlayerOne.Name = "Player A";
            PlayerTwo.Name = "Player B";

            // Players are only placing tiles until false
            PlayerOne.start = true;
            PlayerTwo.start = true;

            // Change the first player's ghosts characters
            foreach (IGhostBase ghost in PlayerOne.StartGhosts)
                    ghost.character = (char)Characters.ghost;

            // Change the second player's ghosts characters
            foreach (IGhostBase ghost in PlayerTwo.StartGhosts)
                ghost.character = (char)Characters.ghost1;

            // Set list of escaped ghosts for each player
            PlayerOne.EscapedGhosts = new List<IGhostBase>();
            PlayerTwo.EscapedGhosts = new List<IGhostBase>();

            // Set up the main scene with the current board
            board.SetUpScene();

            // Set current player to player one
            CurrentPlayer = PlayerOne;

            // Get instance of texts class for printings
            Texts text = Texts.Instance();

            // Write out intro text
            Console.WriteLine(text.IntroText());

            // Get Player input
            Console.ReadLine();

            // Start placing ghosts
            FirstPlay();
        }

        /// <summary>
        /// This method allows us to set the ghosts on the board initially
        /// </summary>
        public void FirstPlay()
        {
            // show player ghosts
            for (int i = 0; i < 2; i++)
            {
                // show board
                board.BoardSetUp();

                // To show each ghost with an index for the player to choose
                int a = 0;

                // Show each ghost
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

                // To place the ghosts again
                PlaceStartGhosts();

                // To allow player two to play twice
                CurrentPlayer = PlayerTwo;

                counter++;
            }

            // Set current player as first player
            CurrentPlayer = PlayerOne;

            // Go to the main loop
            Update();
        }

        /// <summary>
        /// This method allows us to teleport to a mirror when a player moves
        /// to one
        /// </summary>
        public void MirrorTeleport()
        {   
            // get player's input to move
            Console.WriteLine("choose tile to move to");

            IMapElement ChosenTile = inputProcess();

            if(ChosenTile is Mirror)
                ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;
        }

        /// <summary>
        /// This method allows us to display our ghosts on the console
        /// </summary>
        public void DisplayGhosts()
        {
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
        }

        /// <summary>
        /// This void allows us to display the action text for the player
        /// </summary>
        public void DisplayActions()
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

        /// <summary>
        /// This method serves as the main loop for our game
        /// </summary>
        public void Update()
        {
            // While the game isn't over
            while (!Finish)
            {
                // Print the board
                board.BoardSetUp();

                // Show the player's ghosts
                DisplayGhosts();

                // If player is at ghost placing stage
                if (CurrentPlayer.start)
                    PlaceStartGhosts();

                // If player is in-game
                else
                    DisplayActions();
                
                // If player has finished move
                if (PlayDone)
                    Play();

                // To determine when all ghosts were placed
                if (counter >= 18)
                {
                    PlayerOne.start = false;
                    PlayerTwo.start = false;
                }

                // Check if winning conditions have been met
                WinCheck();

                // Clear console for easier visualisation
                Console.Clear();
            }
        }

        /// <summary>
        /// Check if a fhost has died to send it to the dungeon
        /// </summary>
        public void DungeonCheck()
        {
            // If a ghost died
            if (deadGhost != null)
            {
                // Send ghost to dungeon
                SendToDungeon(deadGhost);

                // Check if ghost was blue to rotate portal
                if (deadGhost is BlueGhostPickable)
                    board.bluePortal.CurrentRot = board.bluePortal.Rotate();

                // Check if ghost was red to rotate portal
                if (deadGhost is RedGhostPickable)
                    board.redPortal.CurrentRot = board.redPortal.Rotate();

                // Check if ghost was yellow to rotate portal
                if (deadGhost is YellowGhostPickable)
                    board.yellowPortal.CurrentRot = board.yellowPortal.Rotate();

                // Set the ghost's in-dungeon status to true
                deadGhost.inDungeon = true;

                // Make deadghost null for new checking
                deadGhost = null;
            }
        }

        /// <summary>
        /// This method lets us check if the winning conditions have been met
        /// </summary>
        public void WinCheck()
        {
            // If red ghost escaped
            if (board.CheckRedSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new RedGhostPickable());

            // If blue ghost escaped
            if (board.CheckBlueSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new BlueGhostPickable());

            // If yellow ghost escaped
            if (board.CheckYellowSurrounding())
                CurrentPlayer.EscapedGhosts.Add(new YellowGhostPickable());

            // If ghost of each colour out, player wins
            if (CurrentPlayer.EscapedGhosts.OfType<RedGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<YellowGhostPickable>().Any()
                && CurrentPlayer.EscapedGhosts.OfType<BlueGhostPickable>().Any())
                Finish = true;
        }

        /// <summary>
        /// This method allows us to pick a ghost from the dungeon
        /// </summary>
        public void PickFromDungeon()
        {
            // Ask player for what ghost to remove
            Console.WriteLine("What ghost do you wish to remove?");

            // Get input to remove ghost
            string input = Console.ReadLine();

            string[] inputSplit = input.Split(",");

            int validInputOne;
            int validInputTwo;

            bool InputIsValidOne = Int32.TryParse(inputSplit[0], out validInputOne);
            bool InputIsValidTwo = Int32.TryParse(inputSplit[1], out validInputTwo);

            // If input is valid ask player where to move ghost to
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

                // Place ghost on board
                if (InputIsValidOne2 && InputIsValidTwo2)
                    PlacePiece(board.positions[validInputOne2, validInputTwo2]);
                else
                    PickFromDungeon();
            }

            // Re-enter loop to pick new ghost
            else
                PickFromDungeon();
        }

        /// <summary>
        /// This method allows us to pick a piece from the board and get its info
        /// </summary>
        /// <param name="piece">piece being selected by player</param>
        public void PickPiece()
        {
            // If player isn't at the start of game
            if (!CurrentPlayer.start)
            {
                // If the piece isn't in the dungeon
                if (!CurrentPlayer.ChosenPiece.inDungeon)
                {
                    // Previous tile is piece picked by player
                    PreviousTile = board.positions
                        [CurrentPlayer.ChosenPiece.Pos.X,
                        CurrentPlayer.ChosenPiece.Pos.Y];

                    // piece picked by player is set to null
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

        /// <summary>
        /// Check if the tile moving to is valid
        /// </summary>
        /// <param name="NextTile"></param>
        /// <returns></returns>
        public bool ValidMove(IMapElement NextTile)
        {
            // if the previous tile isn't null (player isn't on dungeon)
            // and position of tile is adjacent to tile
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

            // return false if not valid
            return false;
        }

        /// <summary>
        /// This method lets us place a piece on the board
        /// </summary>
        /// <param name="ChosenTile"></param>
        public virtual void PlacePiece(IMapElement ChosenTile)
        {
            // Check if chosen tile is valid
            CurrentMoveIsValid = ValidMove(ChosenTile);

            // If the piece is in dungeon or start
            if (CurrentPlayer.ChosenPiece.inDungeon || CurrentPlayer.start)
            {
                // If the tile's colour matches the ghost
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

            // If the tile is adjacent
            else if (CurrentMoveIsValid)
            {
                // If the tile isn't empty
                if (!(ChosenTile.PieceOnTile == null))
                {
                    // Fight ghost on tile
                    DeadGhost =
                            CurrentPlayer.ChosenPiece.Fight(ChosenTile.PieceOnTile);

                    // if dead ghost is ghost on chosen tile
                    if (!(DeadGhost == CurrentPlayer.ChosenPiece))
                    {
                        deadGhost = ChosenTile.PieceOnTile;

                        ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                        DungeonCheck();
                    }

                    else
                    { 
                        Console.WriteLine(CurrentPlayer.ChosenPiece);

                        Console.WriteLine(ChosenTile.PieceOnTile);

                        deadGhost = CurrentPlayer.ChosenPiece;

                        DungeonCheck();

                        PreviousTile.PieceOnTile = null;             

                        PlayDone = true;

                    }
                }

                if (ChosenTile is Mirror &&
                    !CurrentPlayer.ChosenPiece.OnMirror)
                {
                    PreviousTile.PieceOnTile = null;

                    CurrentPlayer.ChosenPiece.OnMirror = true;

                    ChosenTile.PieceOnTile = null;

                    Console.WriteLine("You're on a mirror! You can" +
                        "teleport. Choose the mirror coordinates you wish to" +
                        "move to");

                    MirrorTeleport();

                }

                else if (!PlayDone)
                {
                    Console.WriteLine("DEBUG");
                    
                    PreviousTile.PieceOnTile = null;

                    ChosenTile.PieceOnTile = CurrentPlayer.ChosenPiece;

                    PlayDone = true;
                }
            }
        }

        /// <summary>
        /// Send ghost to the dungeon if they died
        /// </summary>
        /// <param name="dungeonGhost"></param>
        public void SendToDungeon(IGhostBase dungeonGhost)
        {
            // Get empty slot in dungeon
            foreach (DungeonSlot slot in board.DungeonSlots)
            {
                // If slot is empty
                if (slot.GhostInSlot == null)
                {
                    // Send ghost to dungeon
                    slot.GhostInSlot = dungeonGhost;

                    // Slot is occupied
                    slot.empty = false;
                    dungeonGhost.inDungeon = true;

                    // finish loop
                    break;
                }
            }
        }

        /// <summary>
        /// This method allows us to place the starting ghosts on the board
        /// </summary>
        public void PlaceStartGhosts()
        {
            Console.WriteLine(CurrentPlayer.Name + " " +
                ", pick one of your ghosts!");

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

        /// <summary>
        /// This method allows us to process the input done by the player
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method allows us to move a ghost from one tile to another
        /// </summary>
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

            NextTile = inputProcess();

            if (NextTile != null)
                PlacePiece(NextTile);
            else
                MoveGhosts();
        }
    }
}