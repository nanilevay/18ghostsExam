using System;
using System.Collections.Generic;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set up the main board for our game
    /// </summary>
    public class GameBoard : IBoardSetup
    {
        /// <summary>
        /// To get the prefab of the yellow portal
        /// </summary>
        public IPortals yellowPortal { get; set; }

        /// <summary>
        /// To get the prefab of the blue portal
        /// </summary>
        public IPortals bluePortal { get; set; }

        /// <summary>
        /// To get the prefab of the red portal
        /// </summary>
        public IPortals redPortal { get; set; }

        /// <summary>
        /// Max number of rows
        /// </summary>
        public int MaxX
        {
            get
            {
                return 5;
            }

            set
            {
            }
        }

        /// <summary>
        /// Max number of columns
        /// </summary>
        public int MaxY
        {
            get
            {
                return 5;
            }

            set
            {
            }
        }

        /// <summary>
        /// Getting all map elements on the board to set
        /// their positions
        /// </summary>
        public IMapElement[,] positions { get; set; }

        /// <summary>
        /// Getting a list of the slots in the dungeon for their positions
        /// </summary>
        public DungeonSlot[,] DungeonSlots { get; set; }

        /// <summary>
        /// This method allows us to set each player's ghosts list
        /// </summary>
        public void SetUpScene()
        {
            positions = new IMapElement[MaxX, MaxY];
            InitialiseBoard();

            DungeonSlots = new DungeonSlot[2, 10];

            SetUpDungeon();
        }

        /// <summary>
        /// This method allows us to set the main map with all the given positions
        /// </summary>
        public void InitialiseBoard()
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
                        {
                            positions[x, y] = new RedPortals();
                            redPortal = positions[x, y] as IPortals;
                        }
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
                        {
                            positions[x, y] = new YellowPortals();
                            yellowPortal = positions[x, y] as IPortals;
                        }
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
                        {
                            positions[x, y] = new BluePortals();
                            bluePortal = positions[x, y] as IPortals;
                        }
                    }

                    positions[x, y].Pos = new Positions(x, y);
                }
            }
        }

        /// <summary>
        /// This method allows us to set up the board by defining each tile in 
        /// its specified position for a new game according to its type
        /// </summary>
        public void BoardSetUp()
        {
            Console.WriteLine("___|_0_||_1_||_2_||_3_||_4_| Y coordinate");

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (y == 0)
                    {
                        Console.Write("_" + x + "_");
                    }

                    if (positions[x, y].PieceOnTile != null)
                    {
                        if (positions[x, y].PieceOnTile is RedGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        if (positions[x, y].PieceOnTile is YellowGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                        if (positions[x, y].PieceOnTile is BlueGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        Console.Write("|_" + positions[x, y].PieceOnTile.character + "_|");
                    }

                    else
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

                        if(positions[x, y] is IPortals)
                            Console.Write("|_" + (char)(positions[x, y] as IPortals).CurrentRot + "_|");
                        else
                            Console.Write("|_" + positions[x, y].Character + "_|");

                    }

                    Console.ResetColor();

                }
                Console.WriteLine();
            }

            Console.WriteLine("___|___||___||___||___||___|");
            Console.WriteLine("X coordinate");

            Console.WriteLine("Dungeon:____________________________________");
            for (int a = 0; a < 2; a++)
            {

                for (int b = 0; b < 9; b++)
                {
                    if (DungeonSlots[a, b].GhostInSlot != null)
                    {
                        if (DungeonSlots[a, b].GhostInSlot is RedGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        if (DungeonSlots[a, b].GhostInSlot is YellowGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }

                        if (DungeonSlots[a, b].GhostInSlot is BlueGhostPickable)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        Console.Write("|_" + DungeonSlots[a, b].GhostInSlot.character + "_|");

                        Console.ResetColor();
                    }

                    else
                    {
                        Console.Write("|_" + DungeonSlots[a, b].Character + "_|");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// This void allows us to set up the dungeon for when ghosts die
        /// </summary>
        public void SetUpDungeon()
        {
            for (int a = 0; a < 2; a++)
            {
                for (int b = 0; b < 9; b++)
                {
                    DungeonSlots[a, b] = new DungeonSlot();
                }
            }
        }

        public bool CheckRedSurrounding()
        {
            if (positions[0, 1].PieceOnTile is RedGhostPickable &&
               redPortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[0, 3].PieceOnTile is BlueGhostPickable &&
                redPortal.CurrentRot == PortalDir.right)
            {
                return true;
            }

            if (positions[1, 2].PieceOnTile is RedGhostPickable &&
                redPortal.CurrentRot == PortalDir.down)
            {
                return true;
            }

            return false;
        }

        public bool CheckBlueSurrounding()
        {
            if (positions[4, 2].PieceOnTile is BlueGhostPickable &&
                bluePortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[3, 2].PieceOnTile is BlueGhostPickable &&
                 bluePortal.CurrentRot == PortalDir.up)
            {
                return true;
            }

            if (positions[1, 4].PieceOnTile is BlueGhostPickable &&
                bluePortal.CurrentRot == PortalDir.right)
            {
                    return true;
            }

                return false;
        }

        public bool CheckYellowSurrounding()
        {

            if (positions[1, 4].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.up)
            {
                return true;
            }

            if (positions[2, 3].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[3, 4].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.down)
            {
                return true;
            }

            return false;
        }

    }
}
